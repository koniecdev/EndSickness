using EndSickness.Shared.Dtos;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogsByMedicineId;
using EndSickness.Shared.Medicines.Queries.GetMedicineById;
using EndSickness.Shared.Medicines.Queries.GetMedicines;

namespace EndSickness.Models.ViewModels;
public record MedicineDosagesViewModel(GetMedicineLogsByMedicineIdVm MedicineLogsList,  GetMedicinesVm MedicineList, MedicineDto Medicine);