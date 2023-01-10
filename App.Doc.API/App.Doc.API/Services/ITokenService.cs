using IdentityModel.Client;

namespace App.Doc.API.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
