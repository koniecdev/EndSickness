using AutoMapper;

namespace EndSickness.Shared.Common.Mappings;

public interface IMapFromDTO<T>
{
    void Mapping(Profile profile) {
        profile.CreateMap(GetType(), typeof(T))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}