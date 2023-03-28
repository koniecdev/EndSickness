using EndSickness.Domain.Common;

namespace EndSickness.Domain.Entities;

public class Medicine : AuditableEntity
{
    private ApplicationUser? applicationUser;
    private ICollection<MedicineList>? medicineLists;

    public Medicine(string name, TimeSpan cooldown, int applicationUserId, int? maxDailyAmount, int? maxTreatmentTime)
    {
        Name = name;
        Cooldown = cooldown;
        ApplicationUserId = applicationUserId;
        MaxDailyAmount = maxDailyAmount;
        MaxTreatmentTime = maxTreatmentTime;
    }
    public string Name { get; private set; }
    public TimeSpan Cooldown { get; private set; }
    public int? MaxDailyAmount { get; private set; }
    public int? MaxTreatmentTime { get; private set; }
    public int ApplicationUserId { get; private set; }
    public virtual ApplicationUser ApplicationUser { 
        get => applicationUser
            ?? throw new UninitializedNavigationPropertyAccessException(nameof(ApplicationUser));
        set => applicationUser = value;
    }
    public virtual ICollection<MedicineList> MedicineLists
    {
        get => medicineLists
            ?? throw new UninitializedNavigationPropertyAccessException(nameof(MedicineLists));
        set => medicineLists = value;
    }
}
