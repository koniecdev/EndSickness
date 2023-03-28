using EndSickness.Application.Common.Interfaces;
using EndSickness.Persistance.EndSicknessDb;
using Microsoft.Extensions.DependencyInjection;

namespace EndSickness.Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services)
    {
        services.AddScoped<IEndSicknessContext, EndSicknessContext>();
        return services;
    }
}