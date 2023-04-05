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
            i.IsGenericType && (i.GetGenericTypeDefinition() == typeof(IMapFromDTO<>) || i.GetGenericTypeDefinition() == typeof(IMapToDTO<>))))
        .ToList();
        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var toDto = type.GetMethod("Mapping") ?? instance!.GetType().GetInterface("IMapToDTO`1")?.GetMethod("Mapping");
            var fromDto = type.GetMethod("Mapping") ?? instance!.GetType().GetInterface("IMapFromDTO`1")?.GetMethod("Mapping");
            
            toDto?.Invoke(instance, new object[] { this });
            fromDto?.Invoke(instance, new object[] { this });
        }
    }
}
