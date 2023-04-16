namespace EndSickness.Shared.MedicineLogs.Commands.CreateMedicineLog;

public record CreateMedicineLogCommand : IRequest<int>, IMapCommand<MedicineLog>
{
    public CreateMedicineLogCommand()
    {
        
    }
    public CreateMedicineLogCommand(int medicineId, DateTime lastlyTaken)
    {
        MedicineId = medicineId;
        LastlyTaken = lastlyTaken;
    }
    public DateTime LastlyTaken { get; init; }
    public int MedicineId { get; init; }
}
