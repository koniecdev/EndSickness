namespace EndSickness.Application.UnitTests.Common.CurrentUserServiceFactories;
internal class FreshCurrentUserFactory : ICurrentUserFactory
{
    public ICurrentUserService Create()
    {
        var currentUserServiceMock = new Mock<ICurrentUserService>();
        currentUserServiceMock.Setup(m => m.AppUserId).Returns("slayId2");
        return currentUserServiceMock.Object;
    }
}
