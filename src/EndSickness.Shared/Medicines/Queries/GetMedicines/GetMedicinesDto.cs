using System.Net;

namespace EndSickness.Shared.Medicines.Queries.GetMedicines;

public record GetMedicinesDto
{
    public GetMedicinesDto()
    {
        Medicines = new List<GetMedicinesMedicineDto>();
    }
    public GetMedicinesDto(ICollection<GetMedicinesMedicineDto> medicines)
    {
        Medicines = medicines;
    }
    public ICollection<GetMedicinesMedicineDto> Medicines { get; init; }
}