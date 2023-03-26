using EndSickness.Domain.Common;

namespace EndSickness.Domain.Entities;

public class ApplicationUser : AuditableEntity
{
    public ApplicationUser(string userId, string email, string username)
    {
        UserId = userId;
        Email = email;
        Username = username;
    }
    public string UserId { get; private set; }
    public string Email { get; private set; }
    public string Username { get; private set; }
    public virtual ICollection<MedicineList>? MedicineLists { get; set; }
    public virtual ICollection<Medicine>? Medicines { get; set; }
}
