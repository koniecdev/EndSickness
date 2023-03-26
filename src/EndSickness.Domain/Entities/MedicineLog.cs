using EndSickness.Domain.Common;

namespace EndSickness.Domain.Entities;

public class MedicineLog : AuditableEntity
{
    private MedicineList? medicineList;
    private Medicine? medicine;

    public MedicineLog(int medicineListId, int medicineId, DateTime lastlyTaken)
    {
        MedicineListId = medicineListId;
        MedicineId = medicineId;
        LastlyTaken = lastlyTaken;
    }
    public DateTime LastlyTaken { get; set; }
    public int MedicineListId { get; set; }
    public virtual MedicineList MedicineList
    { 
        get => medicineList ?? throw new UninitializedNavigationPropertyAccessException(nameof(MedicineList));
        set => medicineList = value;
    }
    public int MedicineId { get; set; }
    public virtual Medicine Medicine 
    { 
        get => medicine ?? throw new UninitializedNavigationPropertyAccessException(nameof(Medicine));
        set => medicine = value;
    }
}
