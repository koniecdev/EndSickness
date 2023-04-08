using EndSickness.Shared.Medicines.Commands.CreateMedicine;

namespace EndSickness.Application.Medicines.Commands.CreateMedicine;

public class CreateMedicineCommandValidator : AbstractValidator<CreateMedicineCommand>
{
    public CreateMedicineCommandValidator()
    {
        RuleFor(m => m.AppUserId).GreaterThan(0).NotEmpty();
    }
}
