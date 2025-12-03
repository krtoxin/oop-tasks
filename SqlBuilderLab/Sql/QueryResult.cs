namespace SqlBuilderLab.Sql;

public sealed class QueryResult
{
    public string CommandText { get; }
    public IReadOnlyDictionary<string, object?> Parameters { get; }

    public QueryResult(string commandText, IReadOnlyDictionary<string, object?> parameters)
    {
        CommandText = commandText;
        Parameters = parameters;
    }
}
