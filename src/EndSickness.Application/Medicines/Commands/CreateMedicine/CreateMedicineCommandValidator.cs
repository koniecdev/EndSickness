using EndSickness.Shared.Medicines.Commands.CreateMedicine;
using FluentValidation;

namespace EndSickness.Application.Medicines.Commands.CreateMedicine;

public class CreateMedicineCommandValidator : AbstractValidator<CreateMedicineCommand>
{
    public CreateMedicineCommandValidator()
    {
        RuleFor(m => m.Name).MinimumLength(2).MaximumLength(100).NotEmpty();
        RuleFor(m => m.Cooldown).GreaterThanOrEqualTo(TimeSpan.Zero).LessThan(TimeSpan.FromDays(200));
        RuleFor(m => m.MaxDailyAmount).GreaterThanOrEqualTo(0).LessThan(30);
        RuleFor(m => m.MaxDaysOfTreatment).GreaterThanOrEqualTo(0);
    }
}
