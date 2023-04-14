using EndSickness.Shared.Medicines.Commands.UpdateMedicine;

namespace EndSickness.Application.Medicines.Commands.UpdateMedicine;

public class UpdateMedicineCommandValidator : AbstractValidator<UpdateMedicineCommand>
{
    public UpdateMedicineCommandValidator()
    {
        RuleFor(m => m.Name).MinimumLength(2).MaximumLength(100);
        RuleFor(m => m.Cooldown).GreaterThan(TimeSpan.FromMinutes(5)).LessThan(TimeSpan.FromDays(200));
        RuleFor(m => m.MaxDailyAmount).GreaterThan(0).LessThan(30);
        RuleFor(m => m.MaxDaysOfTreatment).GreaterThan(1);
    }
}
