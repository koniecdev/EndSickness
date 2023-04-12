using EndSickness.Domain.Common;

namespace EndSickness.Domain.Entities;

public class MedicineLog : AuditableEntity
{
    public MedicineLog(int medicineId, DateTime lastlyTaken)
    {
        MedicineId = medicineId;
        LastlyTaken = lastlyTaken;
    }
    public DateTime LastlyTaken { get; private set; }
    public int MedicineId { get; set; }
    public virtual Medicine Medicine { get; set; } = null!;
}
