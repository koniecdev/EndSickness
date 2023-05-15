using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;

namespace EndSickness.Application.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;

public class DeleteMedicineLogsByMedicineIdCommandValidator : AbstractValidator<DeleteMedicineLogsByMedicineIdCommand>
{
    public DeleteMedicineLogsByMedicineIdCommandValidator()
    {
        RuleFor(m => m.MedicineId).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
    }
}
