namespace EndSickness.Shared.Medicines.Commands.CreateMedicine;

public record CreateMedicineCommand : IRequest<int>, IMapCommand<Medicine>
{
    public CreateMedicineCommand()
    {
        Name = string.Empty;
    }
    public CreateMedicineCommand(string name, TimeSpan cooldown, int appUserId, int? maxDailyAmount, TimeSpan maxDaysOfTreatment)
    {
        Name = name;
        Cooldown = cooldown;
        AppUserId = appUserId;
        MaxDailyAmount = maxDailyAmount;
        MaxDaysOfTreatment = maxDaysOfTreatment;
    }
    public string Name { get; init; }
    public TimeSpan Cooldown { get; init; }
    public int AppUserId { get; init; }
    public int? MaxDailyAmount { get; init; }
    public TimeSpan? MaxDaysOfTreatment { get; init; }
}
