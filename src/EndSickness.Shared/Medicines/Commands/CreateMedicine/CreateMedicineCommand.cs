namespace EndSickness.Shared.Medicines.Commands.CreateMedicine;

public record CreateMedicineCommand : IRequest<int>, IMapCommand<Medicine>
{
    public CreateMedicineCommand()
    {
        Name = string.Empty;
    }
    public CreateMedicineCommand(string name, TimeSpan cooldown, int? maxDailyAmount, int? maxDaysOfTreatment)
    {
        Name = name;
        Cooldown = cooldown;
        MaxDailyAmount = maxDailyAmount;
        MaxDaysOfTreatment = maxDaysOfTreatment;
    }
    public string Name { get; init; }
    public TimeSpan Cooldown { get; init; }
    public int? MaxDailyAmount { get; init; }
    public int? MaxDaysOfTreatment { get; init; }
}
