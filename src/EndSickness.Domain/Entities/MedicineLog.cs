using EndSickness.Domain.Common;

namespace EndSickness.Domain.Entities;

public class MedicineLog : AuditableEntity
{
    public MedicineLog(int appUserId, int medicineId, DateTime lastlyTaken)
    {
        AppUserId = appUserId;
        MedicineId = medicineId;
        LastlyTaken = lastlyTaken;
    }
    public DateTime LastlyTaken { get; private set; }
    public int MedicineId { get; set; }
    public virtual Medicine Medicine { get; set; } = null!;
    public int AppUserId { get; private set; }
    public virtual AppUser AppUser { get; set; } = null!;
}
