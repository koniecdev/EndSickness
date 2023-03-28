﻿using EndSickness.Application.Common.Interfaces;
using EndSickness.Infrastructure.Extensions;
using EndSickness.Persistance.EndSicknessDb;
using Microsoft.Extensions.DependencyInjection;

namespace EndSickness.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services;
    }
}