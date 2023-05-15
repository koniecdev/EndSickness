namespace EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

public record GetMedicineLogsByMedicineIdVm : IMapQuery<MedicineLog>
{
    public GetMedicineLogsByMedicineIdVm()
    {
        MedicineLogs = new List<GetMedicineLogsByMedicineIdDto>();
    }
    public GetMedicineLogsByMedicineIdVm(int medicineId, ICollection<GetMedicineLogsByMedicineIdDto> medicineLogs)
    {
        MedicineLogs = medicineLogs;
        MedicineId = medicineId;
    }
    public int MedicineId { get; init; }
    public ICollection<GetMedicineLogsByMedicineIdDto> MedicineLogs { get; init; }
}