namespace EndSickness.Shared.Medicines.Commands.UpdateMedicine;

public record UpdateMedicineCommand : IRequest, IMapCommand<Medicine>
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public TimeSpan? Cooldown { get; init; }
    public int? AppUserId { get; init; }
    public int? MaxDailyAmount { get; init; }
    public TimeSpan? MaxDaysOfTreatment { get; init; }
}
