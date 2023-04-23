using EndSickness.Shared.Medicines.Commands.UpdateMedicine;

namespace EndSickness.Application.Medicines.Commands.UpdateMedicine;

public class UpdateMedicineCommandValidator : AbstractValidator<UpdateMedicineCommand>
{
    private readonly IEndSicknessContext _db;
    private readonly IResourceOwnershipService _ownershipService;

    public UpdateMedicineCommandValidator(IEndSicknessContext db, IResourceOwnershipService ownershipService)
    {
        _db = db;
        _ownershipService = ownershipService;
        RuleFor(m => m.Id).NotEmpty().GreaterThan(0).LessThan(int.MaxValue)
            .MustAsync(async (id, cancellationToken) => await ValidateOwnershipAsync(id, cancellationToken));
        RuleFor(m => m.Name).MinimumLength(2).MaximumLength(100);
        RuleFor(m => m.HourlyCooldown).GreaterThanOrEqualTo(0).LessThan(1000);
        RuleFor(m => m.MaxDailyAmount).GreaterThanOrEqualTo(0).LessThan(30);
        RuleFor(m => m.MaxDaysOfTreatment).GreaterThanOrEqualTo(0);
    }

    private async Task<bool> ValidateOwnershipAsync(int resourceId, CancellationToken cancellationToken)
    {
        var fromDb = await _db.Medicines.SingleOrDefaultAsync(m => m.StatusId != 0 && m.Id == resourceId, cancellationToken)
            ?? throw new ResourceNotFoundException();
        _ownershipService.CheckOwnership(fromDb.OwnerId);
        return true;
    }
}
