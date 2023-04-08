using EndSickness.Application;
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
    public void IsAuthorized(string objectUserId)
    {
        if (!objectUserId.Equals(AppUserId))
        {
            throw new UnauthorizedAccessException("Sorry, You do not have access to that resource.");
        }
    }
}