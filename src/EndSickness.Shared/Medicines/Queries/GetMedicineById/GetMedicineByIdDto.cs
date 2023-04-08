namespace EndSickness.Shared.Medicines.Queries.GetMedicineById;

public record GetMedicineByIdDto : IMapQuery<Medicine>
{
    public GetMedicineByIdDto(int id, string name, int appUserId, TimeSpan cooldown, int? maxDailyAmount, TimeSpan? maxDaysOfTreatment)
    {
        Id = id;
        Name = name;
        AppUserId = appUserId;
        Cooldown = cooldown;
        MaxDailyAmount = maxDailyAmount;
        MaxDaysOfTreatment = maxDaysOfTreatment;
    }
    public int Id { get; init; }
    public string Name { get; init; }
    public int AppUserId { get; init; }
    public TimeSpan Cooldown { get; init; }
    public int? MaxDailyAmount { get; init; }
    public TimeSpan? MaxDaysOfTreatment { get; init; }
}