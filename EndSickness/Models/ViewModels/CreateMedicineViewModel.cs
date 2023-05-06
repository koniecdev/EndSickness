using EndSickness.Shared.Medicines.Commands.CreateMedicine;

namespace EndSickness.Models.ViewModels;
public class CreateMedicineViewModel
{
    public CreateMedicineViewModel()
    {
        Name = string.Empty;
    }
    public string Name { get; set; }
    public int HourlyCooldown { get; set; }
    public int MaxDailyAmount { get; set; }
    public int MaxDaysOfTreatment { get; set; }
    public CreateMedicineCommand ToCommand()
    {
        return new CreateMedicineCommand(Name, HourlyCooldown, MaxDailyAmount, MaxDaysOfTreatment);
    }
}
