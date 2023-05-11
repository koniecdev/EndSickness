using EndSickness.Application.Common.Exceptions;

namespace EndSickness.Infrastructure.Services;
public class ResourceOwnershipService : IResourceOwnershipService
{
    private readonly ICurrentUserService _currentUserService;

    public ResourceOwnershipService(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public void CheckOwnership(string ownerId)
    {
        if (!_currentUserService.IsAuthorized)
        {
            throw new UnauthorizedAccessException("Please log in first");
        }

        if (ownerId != _currentUserService.AppUserId)
        {
            throw new ForbiddenAccessException();
        }
    }
}
