using EndSickness.Shared.Medicines.Queries.GetDosages;
using IdentityModel.Client;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EndSickness.Services;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IHttpContextAccessor _contextAccessor;

    public RefreshTokenService(IHttpClientFactory clientFactory, IHttpContextAccessor contextAccessor)
    {
        _clientFactory = clientFactory;
        _contextAccessor = contextAccessor;
    }
    public async Task<TokenResponse> Execute()
    {
        if (_contextAccessor.HttpContext is null)
        {
            throw new NullReferenceException();
        }
        var is4client = _clientFactory.CreateClient("IS4");
        var newTokensResponse = await is4client.RequestRefreshTokenAsync(new RefreshTokenRequest()
        {
            Address = "https://localhost:5001/connect/token",
            ClientId = "mvc",
            ClientSecret = "secret",
            RefreshToken = await _contextAccessor.HttpContext.GetTokenAsync("refresh_token")
        });
        if (!string.IsNullOrWhiteSpace(newTokensResponse.AccessToken))
        {
            return newTokensResponse;
        }
        throw new UnauthorizedAccessException();
    }
}