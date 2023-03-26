using EndSickness.Domain.Common;

namespace EndSickness.Domain.Entities;

public class MedicineList : AuditableEntity
{
    public MedicineList(string name)
    {
        Name = name;
    }
    public string Name { get; set; }
    public int ApplicationUserId { get; set; }
    public virtual ApplicationUser? ApplicationUser { get; set; }
    public virtual ICollection<Medicine>? Medicines { get; set; }
}
