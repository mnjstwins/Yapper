# Yapper

Yep Another Wrapper for [Dapper](https://github.com/StackExchange/dapper-dot-net).  This
wrapper provides a simple Unit of Work pattern for isolating transactional business rules,
and a simple CRUD statement builder for easy repository management.



### Unit of Work Operations

``` csharp
using (var db = new DatabaseSession(myIDbConnection))
{
	using (var trx = db.BeginTransaction())
	{
		trx.Commit();
		//	otherwise on Dispose .Rollback by default
		//	if .Commit not already called
	}
}
```


### CRUD Statement Builders (MSSQL Dialect)

Supports both composite keys and identities (requires DataAnnotation decorations).

``` csharp
var sql = StatementBuilder.InsertOne<Member>();
var sql = StatementBuilder.UpdateOne<Member>();
var sql = StatementBuilder.DeleteOne<Member>();
var sql = StatementBuilder.SaveOne<Member>(); /* performs update then insert when not found */
var sql = StatementBuilder.SelectOne<Member>( /*[optional anonymous where]*/ );
var sql = StatementBuilder.SelectMany<Member>( /*[optional anonymous where]*/ );
```

### Sample Usage

``` csharp
public class Member
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MemberId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string MemberName { get; set; }
}

var sql = StatementBuilder.SelectOne<Member>(new { MemberId = 1 });

Member member = Connection.Query<Member>(sql).FirstOrDefault();
```
