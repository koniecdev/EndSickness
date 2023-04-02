using EndSickness.Domain.Common;

namespace EndSickness.Domain.Entities;

public class Medicine : AuditableEntity
{
    public Medicine(string name, TimeSpan cooldown, int appUserId)
    {
        Name = name;
        Cooldown = cooldown;
        AppUserId = appUserId;
    }
    public Medicine(string name, TimeSpan cooldown, int appUserId, int? maxDailyAmount)
    {
        Name = name;
        Cooldown = cooldown;
        AppUserId = appUserId;
        MaxDailyAmount = maxDailyAmount;
    }
    public Medicine(string name, TimeSpan cooldown, int appUserId, TimeSpan? maxDaysOfTreatment)
    {
        Name = name;
        Cooldown = cooldown;
        AppUserId = appUserId;
        MaxDaysOfTreatment = maxDaysOfTreatment;
    }
    public Medicine(string name, TimeSpan cooldown, int appUserId, int? maxDailyAmount, TimeSpan? maxDaysOfTreatment)
    {
        Name = name;
        Cooldown = cooldown;
        AppUserId = appUserId;
        MaxDailyAmount = maxDailyAmount;
        MaxDaysOfTreatment = maxDaysOfTreatment;
    }
    public string Name { get; private set; }
    public TimeSpan Cooldown { get; private set; }
    public int? MaxDailyAmount { get; private set; }
    public TimeSpan? MaxDaysOfTreatment { get; private set; }
    public int AppUserId { get; private set; }
    public virtual AppUser AppUser { get; set; } = null!;
    public virtual ICollection<MedicineList> MedicineLists { get; set; } = null!;
}
