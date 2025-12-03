using System.Linq.Expressions;
using System.Text;

namespace SqlBuilderLab.Sql;

public static class SqlBuilder
{
    public static RootBuilder ForDialect(SqlDialect dialect) => new(dialect);

    public sealed class RootBuilder
    {
        private readonly SqlDialect _dialect;
        internal RootBuilder(SqlDialect dialect) => _dialect = dialect;
        public SelectBuilder<T> Select<T>() => new(_dialect);
    }
}

public sealed class SelectBuilder<T>
{
    private readonly SqlDialect _dialect;
    private readonly List<(string sql, object? value)> _where = new();
    private readonly List<(string col, bool desc)> _order = new();
    private int? _skip;
    private int? _take;
    private int _paramIndex;

    internal SelectBuilder(SqlDialect dialect) => _dialect = dialect;

    public SelectBuilder<T> Where(Expression<Func<T, bool>> predicate)
    {
        if (predicate.Body is MemberExpression me && me.Type == typeof(bool))
        {
            var col = me.Member.Name;
            _where.Add(($"[{col}] = @p{_paramIndex}", true));
            _paramIndex++;
            return this;
        }
        if (predicate.Body is BinaryExpression be)
        {
            var (leftCol, rightVal) = ExtractBinary(be);
            var op = be.NodeType switch
            {
                ExpressionType.Equal => "=",
                ExpressionType.NotEqual => "!=",
                ExpressionType.GreaterThan => ">",
                ExpressionType.GreaterThanOrEqual => ">=",
                ExpressionType.LessThan => "<",
                ExpressionType.LessThanOrEqual => "<=",
                _ => throw new NotSupportedException("Operator not supported")
            };
            _where.Add(($"[{leftCol}] {op} @p{_paramIndex}", rightVal));
            _paramIndex++;
            return this;
        }
        throw new NotSupportedException("Only simple comparisons supported in demo");
    }

    public SelectBuilder<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector)
    {
        _order.Add((ExtractMemberName(keySelector.Body), false));
        return this;
    }
    public SelectBuilder<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector)
    {
        _order.Add((ExtractMemberName(keySelector.Body), true));
        return this;
    }
    public SelectBuilder<T> Skip(int count) { _skip = count; return this; }
    public SelectBuilder<T> Take(int count) { _take = count; return this; }

    public QueryResult Build()
    {
        var table = typeof(T).Name; 
        var sb = new StringBuilder();
        var parameters = new Dictionary<string, object?>();

        if (_dialect == SqlDialect.SqlServer && _take.HasValue && !_skip.HasValue)
        {
            sb.Append($"SELECT TOP(@p{_paramIndex}) * FROM [{table}] ");
            parameters[$"@p{_paramIndex}"] = _take.Value;
            _paramIndex++;
        }
        else
        {
            sb.Append($"SELECT * FROM [{table}] ");
        }

        if (_where.Count > 0)
        {
            sb.Append("WHERE ");
            sb.Append(string.Join(" AND ", _where.Select(w => w.sql)));
            foreach (var (sql, value) in _where)
            {
                var pname = sql.Split('@').Last(); // pN)
                pname = pname.TrimEnd(')', ' ', ';');
                if (!parameters.ContainsKey("@" + pname))
                {
                    parameters["@" + pname] = value;
                }
            }
        }

        if (_order.Count > 0)
        {
            sb.Append(" ORDER BY ");
            sb.Append(string.Join(", ", _order.Select(o => $"[{o.col}]" + (o.desc ? " DESC" : " ASC"))));
        }

        if (_dialect == SqlDialect.PostgreSql)
        {
            if (_take.HasValue)
            {
                sb.Append($" LIMIT @p{_paramIndex}");
                parameters[$"@p{_paramIndex}"] = _take.Value;
                _paramIndex++;
            }
            if (_skip.HasValue)
            {
                sb.Append($" OFFSET @p{_paramIndex}");
                parameters[$"@p{_paramIndex}"] = _skip.Value;
                _paramIndex++;
            }
        }
        else 
        {
            if (_skip.HasValue || (_skip.HasValue && _take.HasValue))
            {
                if (!_order.Any()) sb.Append(" ORDER BY (SELECT 1)");
                sb.Append($" OFFSET @p{_paramIndex} ROWS");
                parameters[$"@p{_paramIndex}"] = _skip.GetValueOrDefault();
                _paramIndex++;
                if (_take.HasValue)
                {
                    sb.Append($" FETCH NEXT @p{_paramIndex} ROWS ONLY");
                    parameters[$"@p{_paramIndex}"] = _take.Value;
                    _paramIndex++;
                }
            }
            else if (_take.HasValue && _skip.HasValue == false)
            {
                
            }
        }

        return new QueryResult(sb.ToString().Trim(), parameters);
    }

    private static (string leftCol, object? rightVal) ExtractBinary(BinaryExpression be)
    {
        var left = be.Left is MemberExpression lm ? lm.Member.Name : throw new NotSupportedException();
        object? value = be.Right switch
        {
            ConstantExpression c => c.Value,
            MemberExpression rm => GetValue(rm),
            _ => Evaluate(be.Right)
        };
        return (left, value);
    }

    private static object? GetValue(MemberExpression me)
    {
        var obj = (me.Expression as ConstantExpression)?.Value;
        if (obj == null) throw new NotSupportedException();
        var fieldInfo = me.Member as System.Reflection.FieldInfo;
        return fieldInfo?.GetValue(obj);
    }

    private static object? Evaluate(Expression expr)
    {
        var lambda = Expression.Lambda(expr);
        return lambda.Compile().DynamicInvoke();
    }

    private static string ExtractMemberName(Expression body) => body switch
    {
        MemberExpression m => m.Member.Name,
        _ => throw new NotSupportedException("Only member access supported")
    };
}
