namespace EndSickness.Shared.Medicines.Queries.GetDosageById;

public record GetDosageByIdVm
{
    public GetDosageByIdVm()
    {
        MedicineName = string.Empty;
    }
    public int MedicineId { get; init; }
    public string MedicineName { get; init; }
    public DateTime LastDose { get; init; }
    public DateTime NextDose { get; init; }
}