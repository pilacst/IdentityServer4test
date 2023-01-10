

using App.Doc.IdentityServer.Data;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Resources;
using System.Security.Claims;

namespace App.Doc.IdentityServer.Config
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var service = new ServiceCollection();
            service.AddLogging();
            service.AddDbContext<AspNetIdentityDbContext>(
                options => options.UseSqlServer(connectionString));

            service.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AspNetIdentityDbContext>()
                .AddDefaultTokenProviders();

            service.AddOperationalDbContext(options =>
            {
                options.ConfigureDbContext = db => db.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
            });

            service.AddConfigurationDbContext(options =>
            {
                options.ConfigureDbContext = db => db.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName));
            });

            var serviceProvider = service.BuildServiceProvider();

            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetService<PersistedGrantDbContext>().Database.Migrate();

            var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            context.Database.Migrate();

            EnsureSeedData(context);

            var ctx = scope.ServiceProvider.GetService<AspNetIdentityDbContext>();
            ctx.Database.Migrate();
            EnsureUsers(scope);
        }


        public static void EnsureSeedData(ConfigurationDbContext context)
        {
            Console.WriteLine("Seeding database...");

            if (!context.Clients.Any())
            {
                Console.WriteLine("Clients being populated");
                foreach (var client in InitialConfig.Clients.ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Clients already populated");
            }

            if (!context.IdentityResources.Any())
            {
                Console.WriteLine("IdentityResources being populated");
                foreach (var resource in InitialConfig.IdentityResources.ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("IdentityResources already populated");
            }

            if (!context.ApiScopes.Any())
            {
                Console.WriteLine("Scopes being populated");
                foreach (var resource in InitialConfig.ApiScopes.ToList())
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Scopes already populated");
            }

            if (!context.ApiResources.Any())
            {
                Console.WriteLine("ApiResources being populated");
                foreach (var resource in InitialConfig.ApiResources.ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("ApiResources already populated");
            }

            Console.WriteLine("Done seeding database.");
            Console.WriteLine();
        }

        public static void EnsureUsers(IServiceScope scope)
        {
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var nayanajith = userMgr.FindByEmailAsync("pilapitiyanss@gmail.com").Result;

            if(nayanajith == null)
            {
                nayanajith = new IdentityUser
                {
                    UserName = "pila",
                    Email = "pilapitiyanss@gmail.com",
                    EmailConfirmed= true
                };

                var result = userMgr.CreateAsync(nayanajith, "abcABC123@@@").Result;

                if (!result.Succeeded)
                    throw new Exception(result.Errors.First().Description);


                result = userMgr.AddClaimsAsync(
                    nayanajith,
                    new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, "Nayanajith Pilapitiya"),
                        new Claim(JwtClaimTypes.GivenName, "Nayanajith"),
                        new Claim(JwtClaimTypes.FamilyName, "Pilapitiya"),
                        new Claim(JwtClaimTypes.WebSite, "https://www.pila.com"),
                        new Claim("Location", "Sri Lanka")
                    }
                    ).Result;

                if (!result.Succeeded)
                    throw new Exception(result.Errors.First().Description);
            }
        }
    }

}