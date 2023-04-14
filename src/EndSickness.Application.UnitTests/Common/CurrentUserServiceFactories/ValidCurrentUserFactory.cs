namespace EndSickness.Application.UnitTests.Common.CurrentUserServiceFactories;
internal class ValidCurrentUserFactory : ICurrentUserFactory
{
    public ICurrentUserService Create()
    {
        var currentUserServiceMock = new Mock<ICurrentUserService>();
        currentUserServiceMock.Setup(m => m.AppUserId).Returns("validUserId");
        currentUserServiceMock.Setup(m => m.IsAuthorized).Returns(true);
        return currentUserServiceMock.Object;
    }
}
