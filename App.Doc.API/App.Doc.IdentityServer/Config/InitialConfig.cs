using IdentityServer4.Models;

namespace App.Doc.IdentityServer.Config
{
    public class InitialConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "role",
                UserClaims = new List<string>{ "role" }
            }
        };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
                new ApiScope("App.Doc.API.Read"),
                new ApiScope("App.Doc.API.Write"),
                new ApiScope("App.Doc.API.Delete"),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new[]
            {
                new ApiResource("App.Doc.API")
                {
                    Scopes = new List<string> { "App.Doc.API.Read" , "App.Doc.API.Write", "App.Doc.API.Delete" },
                    ApiSecrets = new List<Secret> { new Secret("ScopeSecret".Sha512())},
                    UserClaims = new List < string > { "role" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("ClientSecret1".Sha256())},
                    AllowedScopes = { "App.Doc.API.Read", "App.Doc.API.Write", "App.Doc.API.Delete" }
                },
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("InteractiveClienteSecret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https:localhost:5443/signin-oidc" },
                    FrontChannelLogoutUri =  "https:localhost:5443/signout-oidc",
                    PostLogoutRedirectUris = { "https:localhost:5443/signout-callback-oidc" },
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openId", "profile", "App.Doc.API.Read" },
                    RequirePkce = true,
                    RequireConsent = true,
                    AllowPlainTextPkce = false
                }
            };
    }
}
