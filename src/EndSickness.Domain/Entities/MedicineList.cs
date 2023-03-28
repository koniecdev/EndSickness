using EndSickness.Domain.Common;

namespace EndSickness.Domain.Entities;

public class MedicineList : AuditableEntity
{
    private AppUser? applicationUser;
    private ICollection<Medicine>? medicines;

    public MedicineList(string name, int appUserId)
    {
        Name = name;
        AppUserId = appUserId;
    }
    public string Name { get; private set; }
    public int AppUserId { get; private set; }
    public virtual AppUser AppUser
    { 
        get => applicationUser ?? throw new UninitializedNavigationPropertyAccessException(nameof(AppUser));
        set => applicationUser = value;
    }
    public virtual ICollection<Medicine> Medicines
    { 
        get => medicines ?? throw new UninitializedNavigationPropertyAccessException(nameof(Medicines));
        set => medicines = value;
    }
}
