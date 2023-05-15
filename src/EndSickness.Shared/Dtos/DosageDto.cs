namespace EndSickness.Shared.Dtos;

public record DosageDto
{
    public DosageDto()
    {
        MedicineName = string.Empty;
    }
    public DosageDto(int medicineId, string medicineName, DateTime lastDose, DateTime nextDose)
    {
        MedicineId = medicineId;
        MedicineName = medicineName;
        LastDose = lastDose;
        NextDose = nextDose;
    }
    public int MedicineId { get; init; }
    public string MedicineName { get; init; }
    public DateTime LastDose { get; init; }
    public DateTime NextDose { get; init; }
}