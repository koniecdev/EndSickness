﻿namespace EndSickness.Application.UnitTests.Common.CurrentUserServiceFactories;

public interface IResourceOwnershipFactory
{
    public IResourceOwnershipService Create();
}
