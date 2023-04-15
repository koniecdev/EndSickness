namespace EndSickness.Shared.Medicines.Commands.UpdateMedicine;

public record UpdateMedicineCommand : IRequest, IMapCommand<Medicine>
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public int? HourlyCooldown { get; init; }
    public int? MaxDailyAmount { get; init; }
    public int? MaxDaysOfTreatment { get; init; }
}
