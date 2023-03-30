using EndSickness.Infrastructure.Extensions;
using EndSickness.Infrastructure.Services;
using EndSickness.Infrastructure.Services.FileStorage.DirectoryDecorator;
using EndSickness.Infrastructure.Services.FileStorage.FileDecorator;
using Microsoft.Extensions.DependencyInjection;

namespace EndSickness.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAbstractFactory<IDateTime, DateTimeService>();

        services.AddAbstractFactory<ICustomFileSave, CustomFileSaveService>();
        services.AddTransient<ICustomFileSaveDecorator, PreventOverridingCustomFileSaveDecorator>();

        services.AddAbstractFactory<ICustomDirectoryCreate, CustomDirectoryCreateService>();
        services.AddTransient<ICustomDirectoryCreateDecorator, RemoveInvalidCharsCustomDirectoryCreateDecorator>();

        return services;
    }
}