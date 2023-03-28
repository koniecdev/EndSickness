using EndSickness.Infrastructure.AbstractFactory;
using Microsoft.Extensions.DependencyInjection;

namespace EndSickness.Infrastructure.Extensions;

public static class AbstractFactoryExtension
{
    public static IServiceCollection AddAbstractFactory<TInterface, TImplementation>(this IServiceCollection services)
    where TInterface : class
    where TImplementation : class, TInterface
    {
        services.AddTransient<TInterface, TImplementation>();
        services.AddSingleton<Func<TInterface>>(m => () => m.GetService<TInterface>()!);
        services.AddSingleton<IAbstractFactory<TInterface>, AbstractFactory<TInterface>>();

        return services;
    }
}