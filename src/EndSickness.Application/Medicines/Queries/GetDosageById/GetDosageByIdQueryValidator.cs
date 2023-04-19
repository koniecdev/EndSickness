using EndSickness.Shared.Medicines.Queries.GetDosageById;

namespace EndSickness.Application.Medicines.Queries.GetDosageById;

public class GetDosageByIdQueryValidator : AbstractValidator<GetDosageByIdQuery>
{
    private readonly IEndSicknessContext _db;
    private readonly IResourceOwnershipService _ownershipService;

    public GetDosageByIdQueryValidator(IEndSicknessContext db ,IResourceOwnershipService ownershipService)
    {
        RuleFor(m => m.MedicineId).NotEmpty().GreaterThan(0).LessThan(int.MaxValue)
            .MustAsync(async (medicineId, cancellationToken) => await ValidateOwnershipAsync(medicineId, cancellationToken));
        _db = db;
        _ownershipService = ownershipService;
    }

    private async Task<bool> ValidateOwnershipAsync(int medicineId, CancellationToken cancellationToken)
    {
        var fromDb = await _db.Medicines.SingleOrDefaultAsync(m => m.Id == medicineId && m.StatusId != 0, cancellationToken)
            ?? throw new ResourceNotFoundException();
        _ownershipService.CheckOwnership(fromDb.OwnerId);
        return true;
    }
}
