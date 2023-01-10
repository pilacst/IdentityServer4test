# IdentityServer4test

App.Doc.Repository
	Add-Migration InitialMigration
	Update-Database
	
	
Identity server
	Add-Migration InitialAspNetIdentityServerMigration -Context PersistedGrantDbContext
	Add-Migration InitialAspNetIdentityServerMigration -Context ConfigurationDbContext
    Update-Database -Context PersistedGrantDbContext
	Update-Database -Context ConfigurationDbContext
	
	
	Asp.Net identity migration
	Add-Migration InitialAspNetIdentityMigration -Context AspNetIdentityDbContext
    Update-Database -Context AspNetIdentityDbContext
	

Seed data
dotnet run .\App.Doc.IdentityServer\bin\Debug\net6.0\App.Doc.IdentityServer /seed --project App.Doc.IdentityServer


#referance tutorials and articles
https://www.youtube.com/watch?v=SXJ377G5bOg&t=3055s
https://www.youtube.com/watch?v=rNqgxAqGZJ8&t=1752s
https://identityserver4.readthedocs.io/en/latest/
