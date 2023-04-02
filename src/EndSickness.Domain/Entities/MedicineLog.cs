using EndSickness.Domain.Common;

namespace EndSickness.Domain.Entities;

public class MedicineLog : AuditableEntity
{
    public MedicineLog(int medicineListId, int medicineId, DateTime lastlyTaken)
    {
        MedicineListId = medicineListId;
        MedicineId = medicineId;
        LastlyTaken = lastlyTaken;
    }
    public DateTime LastlyTaken { get; private set; }
    public int MedicineListId { get; private set; }
    public virtual MedicineList MedicineList { get; set; } = null!;
    public int MedicineId { get; set; }
    public virtual Medicine Medicine { get; set; } = null!;
}
