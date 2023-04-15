namespace EndSickness.Shared.Medicines.Commands.CreateMedicine;

public record CreateMedicineCommand : IRequest<int>, IMapCommand<Medicine>
{
    public CreateMedicineCommand()
    {
        Name = string.Empty;
    }
    public CreateMedicineCommand(string name, int hourlyCooldown, int maxDailyAmount, int maxDaysOfTreatment)
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
