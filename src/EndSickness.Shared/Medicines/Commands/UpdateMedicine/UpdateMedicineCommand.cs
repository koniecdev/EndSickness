namespace EndSickness.Shared.Medicines.Commands.UpdateMedicine;

public record UpdateMedicineCommand : IRequest, IMapCommand<Medicine>
{
    public UpdateMedicineCommand()
    {
        
    }
    public UpdateMedicineCommand(int id)
    {
        Id = id;
    }
    public int Id { get; private set; }
    public string? Name { get; init; }
    public TimeSpan? Cooldown { get; init; }
    public int? MaxDailyAmount { get; init; }
    public int? MaxDaysOfTreatment { get; init; }
}
