using EndSickness.Shared.Medicines.Queries.GetDosages;
using EndSickness.Shared.Medicines.Queries.GetMedicines;

namespace EndSickness.Models.ViewModels;
public record IndexViewModel(GetDosagesVm DosageList, GetMedicinesVm MedicineList, string Username);