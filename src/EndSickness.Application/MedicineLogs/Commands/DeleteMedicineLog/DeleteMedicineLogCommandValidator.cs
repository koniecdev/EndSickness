using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLog;

namespace EndSickness.Application.MedicineLogs.Commands.DeleteMedicineLog;

public class DeleteMedicineLogCommandValidator : AbstractValidator<DeleteMedicineLogCommand>
{
    public DeleteMedicineLogCommandValidator()
    {
        RuleFor(m => m.Id).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
    }
}
