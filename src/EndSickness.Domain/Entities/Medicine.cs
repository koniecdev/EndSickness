using EndSickness.Domain.Common;

namespace EndSickness.Domain.Entities;

public class Medicine : AuditableEntity
{
    public Medicine()
    {
        Name = string.Empty;
    }
    public Medicine(string name, int hourlyCooldown, int maxDailyAmount,  int maxDaysOfTreatment)
    {
        Name = name;
        HourlyCooldown = hourlyCooldown;
        MaxDailyAmount = maxDailyAmount;
        MaxDaysOfTreatment = maxDaysOfTreatment;
    }
    public string Name { get; private set; }
    public int HourlyCooldown { get; private set; }
    public int MaxDailyAmount { get; private set; }
    public int MaxDaysOfTreatment { get; private set; }
}
