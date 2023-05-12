using EndSickness.Shared.MedicineLogs.Commands.UpdateMedicineLog;

namespace EndSickness.Application.MedicineLogs.Commands.UpdateMedicineLog;

public class UpdateMedicineLogCommandValidator : AbstractValidator<UpdateMedicineLogCommand>
{
    private readonly IDateTime _dateTime;
    public UpdateMedicineLogCommandValidator(IDateTime dateTime)
    {
        _dateTime = dateTime;
        RuleFor(m => m.Id).NotEmpty().GreaterThan(0).LessThan(int.MaxValue);
        RuleFor(m => m.MedicineId).GreaterThan(0).LessThan(int.MaxValue);
        RuleFor(m => m.LastlyTaken)
            .GreaterThanOrEqualTo(new DateTime(2023, 1, 1, 0, 0, 0))
            .LessThanOrEqualTo(_dateTime.Now);
    }
}
