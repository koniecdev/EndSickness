using EndSickness.Shared.Medicines.Queries.GetDosages;
using IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace EndSickness.Services;
public class EndCrudClient<TResponse> : IEndCrudClient<TResponse>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IHttpContextAccessor _accessor;
    private readonly IRefreshTokenService _refreshTokenService;

    public EndCrudClient(IHttpClientFactory clientFactory, IHttpContextAccessor accessor, IRefreshTokenService refreshTokenService)
    {
        _clientFactory = clientFactory;
        _accessor = accessor;
        _refreshTokenService = refreshTokenService;
    }

    public async Task<TResponse> Send(HttpRequestMessage request)
    {
        if (_accessor.HttpContext is null)
        {
            throw new NullReferenceException();
        }
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _accessor.HttpContext.GetTokenAsync("access_token"));
        var client = _clientFactory.CreateClient("EndSickness");
        try
        {
            return await TrySendRequest(client, request);
        }
        catch (UnauthorizedAccessException)
        {
            var newTokens = await _refreshTokenService.Execute();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newTokens.AccessToken);
            await TrySendRequest(client, request);
        }
        throw new UnauthorizedAccessException();
    }

    private async Task<TResponse> TrySendRequest(HttpClient client, HttpRequestMessage request)
    {
        var response = await client.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var stringvm = await response.Content.ReadAsStringAsync();
            var vm = JsonConvert.DeserializeObject<TResponse>(stringvm);
            return vm;
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new UnauthorizedAccessException();
        }
        throw new BadHttpRequestException("There was a problem with connecting to API");
    }
}
