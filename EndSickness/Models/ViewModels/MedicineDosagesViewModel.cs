using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogsByMedicineId;
using EndSickness.Shared.Medicines.Queries.GetDosages;
using EndSickness.Shared.Medicines.Queries.GetMedicineById;
using EndSickness.Shared.Medicines.Queries.GetMedicines;

namespace EndSickness.Models.ViewModels;
public record MedicineDosagesViewModel(GetMedicineLogsByMedicineIdVm MedicineLogsList,  GetMedicinesVm MedicineList, GetMedicineByIdVm Medicine);