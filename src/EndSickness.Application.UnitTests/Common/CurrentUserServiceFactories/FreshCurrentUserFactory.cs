namespace EndSickness.Application.UnitTests.Common.CurrentUserServiceFactories;
internal class FreshCurrentUserFactory : ICurrentUserFactory
{
    public ICurrentUserService Create()
    {
        var currentUserServiceMock = new Mock<ICurrentUserService>();
        currentUserServiceMock.Setup(m => m.AppUserId).Returns("secondUserId");
        currentUserServiceMock.Setup(m => m.IsAuthorized).Returns(true);

        return currentUserServiceMock.Object;
    }
}
