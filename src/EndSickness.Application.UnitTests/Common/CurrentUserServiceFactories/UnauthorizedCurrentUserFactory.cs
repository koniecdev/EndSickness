﻿namespace EndSickness.Application.UnitTests.Common.CurrentUserServiceFactories;
internal class UnauthorizedCurrentUserFactory : ICurrentUserFactory
{
    public ICurrentUserService Create()
    {
        var currentUserServiceMock = new Mock<ICurrentUserService>();
        currentUserServiceMock.Setup(m => m.AppUserId).Returns("NotAuthorizedToResourceAppUserId");
        currentUserServiceMock.Setup(m => m.IsAuthorized(It.IsAny<string>())).Throws<UnauthorizedAccessException>();
        return currentUserServiceMock.Object;
    }
}