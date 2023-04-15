using EndSickness.Shared.Medicines.Commands.CreateMedicine;

namespace EndSickness.Application.Medicines.Commands.CreateMedicine;

public class CreateMedicineCommandValidator : AbstractValidator<CreateMedicineCommand>
{
    public CreateMedicineCommandValidator()
    {
        RuleFor(m => m.Name).MinimumLength(2).MaximumLength(100).NotEmpty();
        RuleFor(m => m.HourlyCooldown).GreaterThanOrEqualTo(0).LessThan(1000);
        RuleFor(m => m.MaxDailyAmount).GreaterThanOrEqualTo(0).LessThan(30);
        RuleFor(m => m.MaxDaysOfTreatment).GreaterThanOrEqualTo(0);
    }
}
