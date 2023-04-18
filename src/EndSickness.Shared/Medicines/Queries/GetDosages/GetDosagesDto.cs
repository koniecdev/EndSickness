namespace EndSickness.Shared.Medicines.Queries.GetDosages;

public record GetDosagesDto
{
    public int MedicineId { get; init; }
    public TimeOnly LastDose { get; init; }
    public TimeOnly NextDose { get; init; }
}