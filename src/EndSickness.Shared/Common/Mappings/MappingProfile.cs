using AutoMapper;
using System.Reflection;

namespace EndSickness.Shared.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes().Where(p =>
        p.GetInterfaces().Any(i =>
            i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IMapCommand<>) || i.GetGenericTypeDefinition() == typeof(IMapQuery<>))))
        .ToList();
        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var queriesMapper = type.GetMethod("Mapping") ?? instance!.GetType().GetInterface("IMapQuery`1")?.GetMethod("Mapping");
            var commandsMapper = type.GetMethod("Mapping") ?? instance!.GetType().GetInterface("IMapCommand`1")?.GetMethod("Mapping");

            queriesMapper?.Invoke(instance, new object[] { this });
            commandsMapper?.Invoke(instance, new object[] { this });
        }
    }
}
