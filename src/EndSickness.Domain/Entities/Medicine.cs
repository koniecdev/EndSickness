using EndSickness.Domain.Common;

namespace EndSickness.Domain.Entities;

public class Medicine : AuditableEntity
{
    private AppUser? applicationUser;
    private ICollection<MedicineList>? medicineLists;

    public Medicine(string name, TimeSpan cooldown, int appUserId)
    {
        Name = name;
        Cooldown = cooldown;
        AppUserId = appUserId;
    }
    public Medicine(string name, TimeSpan cooldown, int appUserId, int? maxDailyAmount, int? maxTreatmentTime)
    {
        Name = name;
        Cooldown = cooldown;
        AppUserId = appUserId;
        MaxDailyAmount = maxDailyAmount;
        MaxTreatmentTime = maxTreatmentTime;
    }
    public string Name { get; private set; }
    public TimeSpan Cooldown { get; private set; }
    public int? MaxDailyAmount { get; private set; }
    public int? MaxTreatmentTime { get; private set; }
    public int AppUserId { get; private set; }
    public virtual AppUser AppUser { 
        get => applicationUser
            ?? throw new UninitializedNavigationPropertyAccessException(nameof(AppUser));
        set => applicationUser = value;
    }
    public virtual ICollection<MedicineList> MedicineLists
    {
        get => medicineLists
            ?? throw new UninitializedNavigationPropertyAccessException(nameof(MedicineLists));
        set => medicineLists = value;
    }
}
