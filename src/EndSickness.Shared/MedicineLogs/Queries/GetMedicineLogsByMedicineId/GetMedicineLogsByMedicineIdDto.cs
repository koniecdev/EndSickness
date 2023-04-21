namespace EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

public record GetMedicineLogsByMedicineIdDto : IMapQuery<MedicineLog>
{
    public DateTime LastlyTaken { get; init; }
}