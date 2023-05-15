using EndSickness.Shared.Dtos;

namespace EndSickness.Shared.Medicines.Queries.GetMedicines;

public record GetMedicinesVm(ICollection<MedicineDto> Medicines);