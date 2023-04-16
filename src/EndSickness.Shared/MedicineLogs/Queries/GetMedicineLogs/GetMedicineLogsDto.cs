using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogById;

namespace EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogs;
public record GetMedicineLogsDto : IMapQuery<MedicineLog>
{
    public GetMedicineLogsDto()
    {

    }
    public GetMedicineLogsDto(int id, DateTime lastlyTaken, int medicineId, GetMedicineLogsMedicineDto medicine)
    {
        Id = id;
        LastlyTaken = lastlyTaken;
        MedicineId = medicineId;
        Medicine = medicine;
    }
    public int Id { get; init; }
    public DateTime LastlyTaken { get; init; }
    public int MedicineId { get; init; }
    public GetMedicineLogsMedicineDto Medicine { get; init; } = null!;
}