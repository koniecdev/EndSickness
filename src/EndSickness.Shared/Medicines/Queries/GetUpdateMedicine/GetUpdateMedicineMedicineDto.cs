using AutoMapper;

namespace EndSickness.Shared.Medicines.Queries.GetUpdateMedicine;

public class GetUpdateMedicineMedicineDto : IMapToDTO<Medicine>
{
    public GetUpdateMedicineMedicineDto()
    {
        Name = string.Empty;
    }
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public string Name { get; set; }
    public TimeSpan Cooldown { get; set; }
    public int? MaxDailyAmount { get; set; }
    public TimeSpan? MaxDaysOfTreatment { get; set; }
    //public void Mapping(Profile profile)
    //{
    //    profile.CreateMap<Medicine, GetUpdateMedicineMedicineDto>()
    //        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    //}
}