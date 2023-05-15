using EndSickness.Application.Common.Behaviours;
using EndSickness.Application.Services.CalculateDosage;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EndSickness.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddScoped<ICalculateNeariestDosageService, CalculateNeariestDosageService>();
        services.AddScoped<IPreventOverdosingService, PreventOverdosingService>();
        return services;
    }
}