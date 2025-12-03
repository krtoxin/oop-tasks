using SqlBuilderLab.Sql;
using SqlBuilderLab.Models;

var pgQuery = SqlBuilder
	.ForDialect(SqlDialect.PostgreSql)
	.Select<User>()
	.Where(u => u.IsActive)
	.Where(u => u.RegistrationDate >= new DateTime(2024, 01, 01))
	.OrderByDescending(u => u.RegistrationDate)
	.Take(10)
	.Build();

Console.WriteLine("PostgreSQL CommandText:\n" + pgQuery.CommandText);
DumpParams(pgQuery);

var msQuery = SqlBuilder
	.ForDialect(SqlDialect.SqlServer)
	.Select<User>()
	.OrderBy(u => u.RegistrationDate)
	.Skip(20)
	.Take(10)
	.Build();

Console.WriteLine("\nSQL Server CommandText:\n" + msQuery.CommandText);
DumpParams(msQuery);

static void DumpParams(QueryResult res)
{
	Console.WriteLine("Parameters:");
	foreach (var p in res.Parameters)
		Console.WriteLine($"  {p.Key} = {p.Value}");
}
