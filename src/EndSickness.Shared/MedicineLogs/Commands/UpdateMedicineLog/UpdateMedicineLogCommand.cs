namespace EndSickness.Shared.MedicineLogs.Commands.UpdateMedicineLog;

public record UpdateMedicineLogCommand : IRequest, IMapCommand<MedicineLog>
{
    public int Id { get; init; }
    public DateTime? LastlyTaken { get; init; }
    public int? MedicineId { get; init; }
}
