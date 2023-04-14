using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EndSickness.Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddAutoMapper((cfg) => { 
            cfg.CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
            cfg.CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
        }, Assembly.GetExecutingAssembly());
        return services;
    }
}