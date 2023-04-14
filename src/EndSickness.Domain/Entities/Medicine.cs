using EndSickness.Domain.Common;

namespace EndSickness.Domain.Entities;

public class Medicine : AuditableEntity
{
    public Medicine(string name)
    {
        Name = name;
    }
    public Medicine(string name, TimeSpan? cooldown)
    {
        Name = name;
        Cooldown = cooldown;
    }
    public Medicine(string name, TimeSpan? cooldown, int? maxDailyAmount,  int? maxDaysOfTreatment)
    {
        Name = name;
        Cooldown = cooldown;
        MaxDailyAmount = maxDailyAmount;
        MaxDaysOfTreatment = maxDaysOfTreatment;
    }
    public string Name { get; private set; }
    public TimeSpan? Cooldown { get; private set; }
    public int? MaxDailyAmount { get; private set; }
    public int? MaxDaysOfTreatment { get; private set; }
}
