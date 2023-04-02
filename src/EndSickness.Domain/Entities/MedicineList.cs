using EndSickness.Domain.Common;

namespace EndSickness.Domain.Entities;

public class MedicineList : AuditableEntity
{
    public MedicineList(string name, int appUserId)
    {
        Name = name;
        AppUserId = appUserId;
    }
    public string Name { get; private set; }
    public int AppUserId { get; private set; }
    public virtual AppUser AppUser { get; set; } = null!;
    public virtual ICollection<Medicine> Medicines { get; set; } = null!;
}
