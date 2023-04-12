namespace EndSickness.Shared.Medicines.Queries.GetMedicineById;

public record GetMedicineByIdVm : IMapQuery<Medicine>
{
    public GetMedicineByIdVm()
    {
        Name = string.Empty;
    }
    public GetMedicineByIdVm(int id, string name, TimeSpan cooldown, int? maxDailyAmount, int? maxDaysOfTreatment)
    {
        Id = id;
        Name = name;
        Cooldown = cooldown;
        MaxDailyAmount = maxDailyAmount;
        MaxDaysOfTreatment = maxDaysOfTreatment;
    }
    public int Id { get; init; }
    public string Name { get; init; }
    public TimeSpan Cooldown { get; init; }
    public int? MaxDailyAmount { get; init; }
    public int? MaxDaysOfTreatment { get; init; }
}