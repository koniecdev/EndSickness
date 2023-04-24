using EndSickness.Shared.Medicines.Queries.GetDosages;
using EndSickness.Shared.Medicines.Queries.GetMedicines;

namespace EndSickness.Models.ViewModels;
public record IndexViewModel
{
    public IndexViewModel(GetDosagesVm dosageLists, GetMedicinesVm medicineLists, string username)
    {
        DosageList = dosageLists;
        MedicineList = medicineLists;
        Username = username;
    }
    public GetDosagesVm DosageList { get; init; }
    public GetMedicinesVm MedicineList { get; init; }
    public string Username { get; init; }
}
