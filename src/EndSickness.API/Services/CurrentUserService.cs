using EndSickness.Application.Common.Interfaces;
using IdentityModel;
using System.Security.Claims;

namespace EndSickness.API.Services;
public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        AppUserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Id) ?? string.Empty;
    }
    public string AppUserId { get; private set; }
    public bool IsAuthorized => !string.IsNullOrWhiteSpace(AppUserId);
}