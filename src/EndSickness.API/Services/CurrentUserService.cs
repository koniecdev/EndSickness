using EndSickness.Application;
using EndSickness.Application.Common.Exceptions;
using IdentityModel;
using System.Security.Claims;

namespace EndSickness.API.Services;
public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        AppUserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtClaimTypes.Id) ?? string.Empty;
    }
    public string AppUserId { get; set; }
    public bool IsAuthorized => !string.IsNullOrWhiteSpace(AppUserId);

    public void CheckOwnership(string ownerId)
    {
        if (!IsAuthorized)
        {
            throw new UnauthorizedAccessException("Please log in first");
        }
        else if (ownerId != AppUserId)
        {
            throw new ForbiddenAccessException();
        }
    }
}