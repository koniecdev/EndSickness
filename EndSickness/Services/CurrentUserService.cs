using IdentityModel;
using System.Security.Claims;

namespace EndSickness.Services;
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId => _httpContextAccessor?.HttpContext?.User.FindFirstValue(JwtClaimTypes.Id) ?? "Unauthorized";
    public string Email => _httpContextAccessor?.HttpContext?.User.FindFirstValue(JwtClaimTypes.Email) ?? "Unauthorized";
    public string Username => _httpContextAccessor?.HttpContext?.User.FindFirstValue(JwtClaimTypes.Name) ?? "Unauthorized";
}
