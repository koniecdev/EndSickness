using IdentityModel.Client;

namespace EndSickness.Services
{
    public interface IRefreshTokenService
    {
        Task<TokenResponse> Execute();
    }
}