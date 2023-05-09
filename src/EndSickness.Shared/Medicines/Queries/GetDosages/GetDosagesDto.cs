namespace EndSickness.Shared.Medicines.Queries.GetDosages;

public record GetDosagesDto
{
    public GetDosagesDto()
    {
        MedicineName = string.Empty;
    }
    public int MedicineId { get; init; }
    public string MedicineName { get; init; }
    public DateTime LastDose { get; init; }
    public DateTime NextDose { get; init; }
}