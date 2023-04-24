using EndSickness.Domain.Entities;
using EndSickness.Shared.MedicineLogs.Commands.UpdateMedicineLog;

namespace EndSickness.Application.MedicineLogs.Commands.UpdateMedicineLog;

public class UpdateMedicineLogCommandValidator : AbstractValidator<UpdateMedicineLogCommand>
{
    private readonly IDateTime _dateTime;
    private readonly IEndSicknessContext _db;
    private readonly IResourceOwnershipService _ownershipService;
    public UpdateMedicineLogCommandValidator(IDateTime dateTime, IEndSicknessContext db, IResourceOwnershipService ownershipService)
    {
        _dateTime = dateTime;
        _db = db;
        _ownershipService = ownershipService;
        RuleFor(m=>m.Id).NotEmpty().GreaterThan(0).LessThan(int.MaxValue)
            .MustAsync(async (id, cancellationToken) => await CheckOwnershipAsync<MedicineLog>(id, cancellationToken));

        RuleFor(m => m.LastlyTaken).GreaterThanOrEqualTo(new DateTime(2023, 1, 1, 0, 0, 0)).LessThanOrEqualTo(_dateTime.Now);
        RuleFor(m => m.MedicineId).GreaterThan(0).LessThan(int.MaxValue)
            .MustAsync(async (id, cancellationToken) => await CheckOwnershipAsync<Medicine>(id, cancellationToken));
    }
    private async Task<bool> CheckOwnershipAsync<TEntity>(int? id, CancellationToken cancellationToken)
    {
        if (id.HasValue)
        {
            OwnershipValidationStrategy validationStrategy = typeof(TEntity).Name switch
            {
                nameof(Medicine) => new MedicineOwnershipValidationStrategy(_db, _ownershipService),
                nameof(MedicineLog) => new MedicineLogOwnershipValidationStrategy(_db, _ownershipService),
                _ => throw new NotImplementedException(),
            };

            return await validationStrategy.Validate(id.Value, cancellationToken);
        }
        return true;
    }
}
