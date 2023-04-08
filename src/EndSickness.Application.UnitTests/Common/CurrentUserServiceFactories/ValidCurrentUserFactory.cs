namespace EndSickness.Application.UnitTests.Common.CurrentUserServiceFactories;
internal class ValidCurrentUserFactory : ICurrentUserFactory
{
    public ICurrentUserService Create()
    {
        var currentUserServiceMock = new Mock<ICurrentUserService>();
        currentUserServiceMock.Setup(m => m.AppUserId).Returns("slayId");
        return currentUserServiceMock.Object;
    }
}
