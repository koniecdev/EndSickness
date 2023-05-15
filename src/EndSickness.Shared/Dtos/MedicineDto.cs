namespace EndSickness.Shared.Dtos;

public record MedicineDto : IMapQuery<Medicine>
{
    public MedicineDto()
    {
        Name = string.Empty;
    }
    public MedicineDto(int id, string name, int hourlyCooldown, int maxDailyAmount, int maxDaysOfTreatment)
    {
        Id = id;
        Name = name;
        HourlyCooldown = hourlyCooldown;
        MaxDailyAmount = maxDailyAmount;
        MaxDaysOfTreatment = maxDaysOfTreatment;
    }
    public int Id { get; init; }
    public string Name { get; init; }
    public int HourlyCooldown { get; init; }
    public int MaxDailyAmount { get; init; }
    public int MaxDaysOfTreatment { get; init; }
}