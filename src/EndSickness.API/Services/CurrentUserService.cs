using EndSickness.Application;
using EndSickness.Application.Common.Interfaces;
using IdentityModel;
using System.Security.Claims;

namespace EndSickness.API.Services;
public class CurrentUserService : ICurrentUserService
{
    public string AppUserId { get; set; }
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        AppUserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Id) ?? string.Empty;
    }
}