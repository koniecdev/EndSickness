using EndSickness.Application.Common.Interfaces;
using EndSickness.Persistance.EndSicknessDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EndSickness.Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EndSicknessContext>(o => o.UseSqlServer(configuration.GetConnectionString("DefaultDatabase")));
        services.AddScoped<IEndSicknessContext, EndSicknessContext>();
        return services;
    }
}