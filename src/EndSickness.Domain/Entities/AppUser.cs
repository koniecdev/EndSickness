using EndSickness.Domain.Common;

namespace EndSickness.Domain.Entities;

public class AppUser : AuditableEntity
{
    public AppUser(string userId, string email, string username)
    {
        UserId = userId;
        Email = email;
        Username = username;
    }
    public string UserId { get; private set; }
    public string Email { get; private set; }
    public string Username { get; private set; }

    public virtual ICollection<MedicineList> MedicineLists { get; set; } = null!;
    public virtual ICollection<Medicine> Medicines { get; set; } = null!;
}