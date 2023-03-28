using EndSickness.Domain.Common;

namespace EndSickness.Domain.Entities;

public class MedicineList : AuditableEntity
{
    private ApplicationUser? applicationUser;
    private ICollection<Medicine>? medicines;

    public MedicineList(string name)
    {
        Name = name;
    }
    public string Name { get; private set; }
    public int ApplicationUserId { get; private set; }
    public virtual ApplicationUser ApplicationUser
    { 
        get => applicationUser ?? throw new UninitializedNavigationPropertyAccessException(nameof(ApplicationUser));
        set => applicationUser = value;
    }
    public virtual ICollection<Medicine> Medicines
    { 
        get => medicines ?? throw new UninitializedNavigationPropertyAccessException(nameof(Medicines));
        set => medicines = value;
    }
}
