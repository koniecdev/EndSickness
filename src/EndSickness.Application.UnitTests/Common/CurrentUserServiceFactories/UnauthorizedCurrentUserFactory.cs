using EndSickness.Application.Common.Interfaces;

namespace EndSickness.Application.UnitTests.Common.CurrentUserServiceFactories;
internal class UnauthorizedCurrentUserFactory : ICurrentUserFactory
{
    public ICurrentUserService Create()
    {
        var currentUserServiceMock = new Mock<ICurrentUserService>();
        currentUserServiceMock.Setup(m => m.AppUserId).Returns("");
        currentUserServiceMock.Setup(m => m.IsAuthorized).Returns(false);
        return currentUserServiceMock.Object;
    }
}
