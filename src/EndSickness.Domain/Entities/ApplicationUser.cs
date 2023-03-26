using EndSickness.Domain.Common;
using System.Data;
using System.Net;

namespace EndSickness.Domain.Entities;

public class ApplicationUser : AuditableEntity
{
    private ICollection<MedicineList>? medicineLists;
    private ICollection<Medicine>? medicines;

    public ApplicationUser(string userId, string email, string username)
    {
        UserId = userId;
        Email = email;
        Username = username;
    }
    public string UserId { get; private set; }
    public string Email { get; private set; }
    public string Username { get; private set; }

    public virtual ICollection<MedicineList> MedicineLists
    {
        get => medicineLists
               ?? throw new UninitializedNavigationPropertyAccessException(nameof(MedicineLists));
        set => medicineLists = value;
    }
    public virtual ICollection<Medicine> Medicines
    {
        get => medicines
               ?? throw new UninitializedNavigationPropertyAccessException(nameof(Medicines));
        set => medicines = value;
    }
}