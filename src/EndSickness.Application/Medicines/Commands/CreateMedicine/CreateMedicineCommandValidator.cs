using EndSickness.Shared.Medicines.Commands.CreateMedicine;

namespace EndSickness.Application.Medicines.Commands.CreateMedicine;

public class CreateMedicineCommandValidator : AbstractValidator<CreateMedicineCommand>
{
    public CreateMedicineCommandValidator()
    {
        RuleFor(m => m.Name).MinimumLength(2).MaximumLength(100).NotEmpty();
        RuleFor(m => m.Cooldown).GreaterThan(TimeSpan.FromMinutes(5)).LessThan(TimeSpan.FromDays(200)).NotEmpty();
        RuleFor(m => m.AppUserId).GreaterThan(0).NotEmpty();
        RuleFor(m => m.MaxDailyAmount).GreaterThan(0).LessThan(30);
        RuleFor(m => m.MaxDaysOfTreatment).GreaterThan(TimeSpan.FromDays(1));
    }
}
