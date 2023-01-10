using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace App.Doc.API.Services
{
    public class TokenService : ITokenService
    {
        public readonly IOptions<IdentityServerSettings> identityServerSettings;
        public readonly DiscoveryDocumentResponse discoveryDocumentResponse;
        public readonly HttpClient httpClient;

        public TokenService(IOptions<IdentityServerSettings> identityServerSettings)
        {
            this.identityServerSettings = identityServerSettings;
            

        }

        public async Task<DiscoveryDocumentResponse> GetDiscoveryDocument()
        {
            var client = new HttpClient();
            var discoveryDocument = await client.GetDiscoveryDocumentAsync(identityServerSettings.Value.DiscoveryUrl);

            if (discoveryDocument.IsError)
                throw new Exception("Discovery document error", discoveryDocument.Exception);

            return discoveryDocument;
        }

        public async Task<TokenResponse> GetToken(string scope)
        {
            //var document = await GetDiscoveryDocument();
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Scope = scope,
                Address = "https://localhost:5443/connect/token",//document.TokenEndpoint,
                ClientId = identityServerSettings.Value.ClientName,
                ClientSecret = identityServerSettings.Value.ClientPassword
            });

            if (tokenResponse.IsError)
                throw new Exception("Token response error", tokenResponse.Exception);

            return tokenResponse;
        }
    }
}
