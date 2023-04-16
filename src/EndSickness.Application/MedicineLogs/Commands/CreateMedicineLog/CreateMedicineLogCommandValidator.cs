using EndSickness.Shared.MedicineLogs.Commands.CreateMedicineLog;

namespace EndSickness.Application.MedicineLogs.Commands.CreateMedicineLog;

public class CreateMedicineLogCommandValidator : AbstractValidator<CreateMedicineLogCommand>
{
    private readonly IDateTime _dateTime;
    public CreateMedicineLogCommandValidator(IDateTime dateTime)
    {
        _dateTime = dateTime;
        RuleFor(m => m.LastlyTaken).GreaterThanOrEqualTo(new DateTime(2023, 1, 1, 0, 0, 0)).LessThanOrEqualTo(_dateTime.Now).NotEmpty();
        RuleFor(m => m.MedicineId).GreaterThan(0).LessThan(int.MaxValue).NotEmpty();
    }
}
