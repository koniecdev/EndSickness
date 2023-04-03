namespace EndSickness.Shared.Medicines.Queries.GetAll;

public class GetAllMedicinesVm
{
    public GetAllMedicinesVm()
    {
        Medicines = new List<MedicineDto>();
    }
    public ICollection<MedicineDto> Medicines { get; set; }
}