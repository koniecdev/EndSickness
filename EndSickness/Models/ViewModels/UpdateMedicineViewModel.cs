using EndSickness.Shared.Medicines.Commands.UpdateMedicine;

namespace EndSickness.Models.ViewModels;
public class UpdateMedicineViewModel
{
    public UpdateMedicineViewModel()
    {
    }
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? HourlyCooldown { get; set; }
    public int? MaxDailyAmount { get; set; }
    public int? MaxDaysOfTreatment { get; set; }
    public UpdateMedicineCommand ToCommand()
    {
        return new UpdateMedicineCommand()
        {
            Id = Id,
            Name = Name,
            HourlyCooldown = HourlyCooldown,
            MaxDailyAmount = MaxDailyAmount,
            MaxDaysOfTreatment = MaxDaysOfTreatment
        };
    }
}
