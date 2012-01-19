Introduction
============
Dragon is a simplified ORM framework. It has similar syntax to Matrix.Data.Database, but map to static types, not dynamic. E.g.

```C#
Database database = Database.Open("Users");
var users = database.Query<TestUser>("select UserId, Email from UserProfile where UserId=@UserId",
				new { UserId = 2 }).ToList();
Assert.AreEqual(2, users[0].UserId);
Assert.AreEqual("user@company.com", users[0].Email);
```

Database Methods
================
The Database Class has methods Open, Execute QueryValue and Query.

* Open, opens database connection, the parameter is the connect string, or the connection name in app.config file or the registry path to a connection string
* Execute, creates a command and run ExecuteNonQuery
* QueryValue, creates a command and run ExecuteScalar
* Query, creates a command with SQL statements or stored procedure name, creates objects. Query method supports up to 3 multiple record sets, the result objects are create in a Tuple, E.g.

```C#
Database database = Database.Open("Users");
var users = database.Query<TestUser, TestMemberShip, TestUserRole>(
	@"select * from dbo.UserProfile
		select * from dbo.webpages_Membership
		select * from dbo.webpages_Roles");

Assert.AreEqual(1, users.Item1.ToList()[0].UserId);
Assert.AreEqual(0, users.Item2.Count());
Assert.AreEqual("Sysadmin", users.Item3.ToList()[0].RoleName);
```

Source Code
===========
https://github.com/yysun/Dragon

