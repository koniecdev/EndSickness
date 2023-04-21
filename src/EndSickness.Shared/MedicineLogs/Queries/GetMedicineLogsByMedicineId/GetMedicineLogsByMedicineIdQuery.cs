namespace EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

public record GetMedicineLogsByMedicineIdQuery : IRequest<GetMedicineLogsByMedicineIdVm>
{
    public GetMedicineLogsByMedicineIdQuery(int medicineId)
    {
        MedicineId = medicineId;
    }
    public int MedicineId { get; init; }
}
