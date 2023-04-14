using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.Common.Interfaces;

namespace EndSickness.Application.UnitTests.Common.CurrentUserServiceFactories;
internal class ResourceOwnershipServiceUnauthorizedFactory : IResourceOwnershipFactory
{
    public IResourceOwnershipService Create()
    {
        var resourceOwnershipServiceMock = new Mock<IResourceOwnershipService>();
        resourceOwnershipServiceMock.Setup(m => m.CheckOwnership(It.IsAny<string>())).Throws<UnauthorizedAccessException>();
        return resourceOwnershipServiceMock.Object;
    }
}
