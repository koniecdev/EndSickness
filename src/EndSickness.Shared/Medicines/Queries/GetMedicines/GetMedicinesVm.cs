namespace EndSickness.Shared.Medicines.Queries.GetMedicines;

public record GetMedicinesVm
{
    public GetMedicinesVm()
    {
        Medicines = new List<GetMedicinesDto>();
    }
    public GetMedicinesVm(ICollection<GetMedicinesDto> medicines)
    {
        Medicines = medicines;
    }
    public ICollection<GetMedicinesDto> Medicines { get; init; }
}