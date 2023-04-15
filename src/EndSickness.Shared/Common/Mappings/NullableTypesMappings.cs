using AutoMapper;

namespace EndSickness.Shared.Common.Mappings;

public class NullableTypesMappings
{
    public static void Mapping(Profile profile)
    {
        profile.CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
        profile.CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);
    }
}
