# This project is mainly to study identity with asp.net core mvc 

## App.Doc.Repository project
``` Add-Migration InitialMigration ``` <br />
``` Update-Database ```
	
	
## Identity server project
``` Add-Migration InitialAspNetIdentityServerMigration -Context PersistedGrantDbContext ``` <br />
``` Add-Migration InitialAspNetIdentityServerMigration -Context ConfigurationDbContext ``` <br />
``` Update-Database -Context PersistedGrantDbContext ``` <br />
``` Update-Database -Context ConfigurationDbContext ``` <br />
	
	
## Asp.Net identity migration
``` Add-Migration InitialAspNetIdentityMigration -Context AspNetIdentityDbContext ``` <br />
``` Update-Database -Context AspNetIdentityDbContext ``` <br />
	

## Seed data
``` dotnet run .\App.Doc.IdentityServer\bin\Debug\net6.0\App.Doc.IdentityServer /seed --project App.Doc.IdentityServer ``` <br />


## Referance tutorials and articles
https://www.youtube.com/watch?v=SXJ377G5bOg&t=3055s
https://www.youtube.com/watch?v=rNqgxAqGZJ8&t=1752s
https://identityserver4.readthedocs.io/en/latest/
