using EndSickness.Shared.Medicines.Commands.DeleteMedicine;

namespace EndSickness.Application.Medicines.Commands.DeleteMedicine;

public class DeleteMedicineCommandValidator : AbstractValidator<DeleteMedicineCommand>
{
    private readonly IEndSicknessContext _db;
    private readonly IResourceOwnershipService _ownershipService;

    public DeleteMedicineCommandValidator(IEndSicknessContext db, IResourceOwnershipService ownershipService)
    {
        RuleFor(m => m.Id).NotEmpty().GreaterThan(0).LessThan(int.MaxValue)
            .MustAsync(async (id, cancellationToken) => await ValidateOwnershipAsync(id, cancellationToken));
        _db = db;
        _ownershipService = ownershipService;
    }

    private async Task<bool> ValidateOwnershipAsync(int resourceId, CancellationToken cancellationToken)
    {
        var fromDb = await _db.Medicines.SingleOrDefaultAsync(m => m.StatusId != 0 && m.Id == resourceId, cancellationToken)
            ?? throw new ResourceNotFoundException();
        _ownershipService.CheckOwnership(fromDb.OwnerId);
        return true;
    }
}
