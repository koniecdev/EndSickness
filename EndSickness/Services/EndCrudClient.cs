using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EndSickness.Services;
public class EndCrudClient<TResponse> : IEndCrudClient<TResponse>
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IHttpContextAccessor _accessor;
    private readonly IRefreshTokenService _refreshTokenService;
    private HttpClient? _client;

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
        _client = _clientFactory.CreateClient("EndSickness");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _accessor.HttpContext.GetTokenAsync("access_token"));
        try
        {
            return await TrySendRequest(request);
        }
        catch (UnauthorizedAccessException)
        {
            var newTokens = await _refreshTokenService.Execute();
            var newRequest = new HttpRequestMessage(request.Method, request.RequestUri);
            newRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newTokens.AccessToken);
            return await TrySendRequest(newRequest);
        }
        throw new UnauthorizedAccessException();
    }

    private async Task<TResponse> TrySendRequest(HttpRequestMessage request)
    {
        var response = await _client?.SendAsync(request)!;
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
