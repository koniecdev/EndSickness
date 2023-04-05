using AutoMapper;

namespace EndSickness.Shared.Common.Mappings;

public interface IMapToDTO<T>
{
    void Mapping(Profile profile) {
        profile.CreateMap(typeof(T), GetType())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}