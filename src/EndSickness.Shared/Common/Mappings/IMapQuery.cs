using AutoMapper;

namespace EndSickness.Shared.Common.Mappings;

public interface IMapQuery<TDestinationEntity>
{
    void Mapping(Profile profile) {
        profile.CreateMap(GetType(), typeof(TDestinationEntity))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}