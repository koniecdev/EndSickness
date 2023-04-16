namespace EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogById;

public record GetMedicineLogByIdVm : IMapQuery<MedicineLog>
{
    public GetMedicineLogByIdVm()
    {
        
    }
    public GetMedicineLogByIdVm(int id, DateTime lastlyTaken, int medicineId, GetMedicineLogByIdDto medicine)
    {
        Id = id;
        LastlyTaken = lastlyTaken;
        MedicineId = medicineId;
        Medicine = medicine;
    }
    public int Id { get; init; }
    public DateTime LastlyTaken { get; init; }
    public int MedicineId { get; init; }
    public GetMedicineLogByIdDto Medicine { get; init; } = null!;
}