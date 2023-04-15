using EndSickness.Application.Common.Exceptions;

namespace EndSickness.Application.UnitTests.Common.CurrentUserServiceFactories;
internal class ResourceOwnershipServiceInvalidUserFactory : IResourceOwnershipFactory
{
    public IResourceOwnershipService Create()
    {
        var resourceOwnershipServiceMock = new Mock<IResourceOwnershipService>();
        resourceOwnershipServiceMock.Setup(m => m.CheckOwnership(It.IsAny<string>())).Throws<ForbiddenAccessException>();
        return resourceOwnershipServiceMock.Object;
    }
}
