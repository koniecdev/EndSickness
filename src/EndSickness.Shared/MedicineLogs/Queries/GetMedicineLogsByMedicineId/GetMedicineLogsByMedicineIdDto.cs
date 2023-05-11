namespace EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

public record GetMedicineLogsByMedicineIdDto : IMapQuery<MedicineLog>
{
    public int Id { get; init; }
    public DateTime LastlyTaken { get; init; }
}