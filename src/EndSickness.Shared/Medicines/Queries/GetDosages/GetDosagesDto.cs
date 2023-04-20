namespace EndSickness.Shared.Medicines.Queries.GetDosages;

public record GetDosagesDto
{
    public GetDosagesDto()
    {
        MedicineName = string.Empty;
    }
    public int MedicineId { get; init; }
    public string MedicineName { get; init; }
    public TimeOnly LastDose { get; init; }
    public TimeOnly NextDose { get; init; }
}