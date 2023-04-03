namespace EndSickness.Shared.Medicines.Queries.GetAll;

public class MedicineDto
{
    public MedicineDto()
    {
        Name = string.Empty;
    }
    public string Name { get; set; }
    public TimeSpan Cooldown { get; set; }
    public int? MaxDailyAmount { get; set; }
    public TimeSpan? MaxDaysOfTreatment { get; set; }
}