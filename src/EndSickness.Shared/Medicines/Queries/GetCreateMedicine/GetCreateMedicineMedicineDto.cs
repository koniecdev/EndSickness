namespace EndSickness.Shared.Medicines.Queries.GetCreateMedicine;

public class GetCreateMedicineMedicineDto
{
    public GetCreateMedicineMedicineDto()
    {
        Name = string.Empty;
    }
    public int AppUserId { get; set; }
    public string Name { get; set; }
    public TimeSpan Cooldown { get; set; }
    public int? MaxDailyAmount { get; set; }
    public TimeSpan? MaxDaysOfTreatment { get; set; }
}