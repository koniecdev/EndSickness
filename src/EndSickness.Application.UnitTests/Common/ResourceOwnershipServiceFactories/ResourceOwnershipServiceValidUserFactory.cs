using EndSickness.Application.Common.Exceptions;

namespace EndSickness.Application.UnitTests.Common.CurrentUserServiceFactories;
internal class ResourceOwnershipServiceValidUserFactory : IResourceOwnershipFactory
{
    public IResourceOwnershipService Create()
    {
        var currentUserServiceMock = new Mock<IResourceOwnershipService>();
        //currentUserServiceMock.Setup(m => m.CheckOwnership(It.IsAny<string>()));

        return currentUserServiceMock.Object;
    }
}
