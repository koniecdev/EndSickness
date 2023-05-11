using EndSickness.Shared.Medicines.Commands.UpdateMedicine;

namespace EndSickness.Application.Medicines.Commands.UpdateMedicine;

public class UpdateMedicineCommandValidator : AbstractValidator<UpdateMedicineCommand>
{
    public UpdateMedicineCommandValidator()
    {
        RuleFor(m => m.Id).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
        RuleFor(m => m.Name).MinimumLength(2).MaximumLength(100);
        RuleFor(m => m.HourlyCooldown).GreaterThanOrEqualTo(0).LessThan(1000);
        RuleFor(m => m.MaxDailyAmount).GreaterThanOrEqualTo(0).LessThan(30);
        RuleFor(m => m.MaxDaysOfTreatment).GreaterThanOrEqualTo(0);
    }
}
