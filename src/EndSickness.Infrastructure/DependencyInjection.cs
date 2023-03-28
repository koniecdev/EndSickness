using EndSickness.Application.Common.Interfaces;
using EndSickness.Infrastructure.Extensions;
using EndSickness.Persistance.EndSicknessContext;
using Microsoft.Extensions.DependencyInjection;

namespace EndSickness.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAbstractFactory<IEndSicknessContext, EndSicknessContext>();
        return services;
    }
}