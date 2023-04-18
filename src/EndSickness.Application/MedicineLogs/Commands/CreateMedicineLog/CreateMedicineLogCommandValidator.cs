using EndSickness.Shared.MedicineLogs.Commands.CreateMedicineLog;
using FluentValidation;

namespace EndSickness.Application.MedicineLogs.Commands.CreateMedicineLog;

public class CreateMedicineLogCommandValidator : AbstractValidator<CreateMedicineLogCommand>
{
    private readonly IDateTime _dateTime;
    private readonly IEndSicknessContext _db;
    private readonly IResourceOwnershipService _ownershipService;

    public CreateMedicineLogCommandValidator(IDateTime dateTime, IEndSicknessContext db, IResourceOwnershipService ownershipService)
    {
        _dateTime = dateTime;
        _db = db;
        _ownershipService = ownershipService;
        
        RuleFor(m => m.MedicineId).GreaterThan(0).LessThan(int.MaxValue).NotEmpty();
        RuleFor(m => m.LastlyTaken)
            .NotEmpty()
            .MustAsync(async(command, newDoseDateTime, cancellationToken) => await PreventOverdosingAsync(command, newDoseDateTime, cancellationToken))
            .WithMessage("We do not support overdosing of medicine.");
    }

    private async Task<bool> PreventOverdosingAsync(CreateMedicineLogCommand command, DateTime newDoseDateTime, CancellationToken cancellationToken)
    {
        if (newDoseDateTime < new DateTime(2023, 1, 1) || newDoseDateTime > _dateTime.Now)
        {
            throw new ValidationException("You have provided date older than 2023 or date in the future.");
        }
        var operatingMedicine = await _db.Medicines.FirstOrDefaultAsync(m => m.Id == command.MedicineId, cancellationToken)
            ?? throw new ResourceNotFoundException();
        var previousMedicineLogs = await _db.MedicineLogs.Where(m => m.MedicineId == command.MedicineId).ToListAsync(cancellationToken);

        if(previousMedicineLogs.Count > 0)
        {
            _ownershipService.CheckOwnership(previousMedicineLogs.First().OwnerId);
            var latestLog = previousMedicineLogs.OrderByDescending(m => m.LastlyTaken).First();
            if (latestLog.LastlyTaken + TimeSpan.FromHours(operatingMedicine.HourlyCooldown) <= newDoseDateTime)
            {
                var logsFromDayBeforeNewDose = previousMedicineLogs.Where(m => m.LastlyTaken > newDoseDateTime - TimeSpan.FromHours(24)).ToList();
                if(logsFromDayBeforeNewDose.Count != operatingMedicine.MaxDailyAmount)
                {
                    return true;
                }
                else
                {
                    throw new OverdoseException();
                }
            }
            else
            {
                throw new OverdoseException();
            }
        }
        else
        {
            return true;
        }
    }
}
