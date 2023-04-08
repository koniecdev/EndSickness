namespace EndSickness.Shared.Medicines.Queries.GetMedicines;

public record GetMedicinesDto
{
    public GetMedicinesDto(ICollection<GetMedicinesMedicineDto> medicines)
    {
        Medicines = medicines;
    }
    public ICollection<GetMedicinesMedicineDto> Medicines { get; init; }
}