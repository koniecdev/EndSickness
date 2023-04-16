﻿namespace EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogById;

public record GetMedicineLogByIdDto : IMapQuery<Medicine>
{
    public GetMedicineLogByIdDto()
    {
        Name = string.Empty;
    }
    public GetMedicineLogByIdDto(string name, int hourlyCooldown, int maxDailyAmount, int maxDaysOfTreatment)
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