namespace EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogs;

public record GetMedicineLogsDto : IMapQuery<MedicineLog>
{
    public GetMedicineLogsDto()
    {
        Name = string.Empty;
    }
    public GetMedicineLogsDto(string name, int hourlyCooldown, int maxDailyAmount, int maxDaysOfTreatment)
    {
        Name = name;
        HourlyCooldown = hourlyCooldown;
        MaxDailyAmount = maxDailyAmount;
        MaxDaysOfTreatment = maxDaysOfTreatment;
    }
    public string Name { get; init; }
    public int HourlyCooldown { get; init; }
    public int MaxDailyAmount { get; init; }
    public int MaxDaysOfTreatment { get; init; }
}