namespace EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;

public record DeleteMedicineLogsByMedicineIdCommand : IRequest
{
    public DeleteMedicineLogsByMedicineIdCommand()
    {

    }
    public DeleteMedicineLogsByMedicineIdCommand(int medicineId)
    {
        MedicineId = medicineId;
    }
    public int MedicineId { get; init; }
}
