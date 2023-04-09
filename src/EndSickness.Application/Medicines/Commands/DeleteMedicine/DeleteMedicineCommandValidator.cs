using EndSickness.Shared.Medicines.Commands.DeleteMedicine;

namespace EndSickness.Application.Medicines.Commands.DeleteMedicine;

public class DeleteMedicineCommandValidator : AbstractValidator<DeleteMedicineCommand>
{
    public DeleteMedicineCommandValidator()
    {
        RuleFor(m => m.Id).GreaterThan(0).LessThan(int.MaxValue).NotEmpty();
    }
}
