using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

namespace EndSickness.Application.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

public class GetMedicineLogsByMedicineIdQueryValidator : AbstractValidator<GetMedicineLogsByMedicineIdQuery>
{
    private readonly IEndSicknessContext _db;
    private readonly IResourceOwnershipService _ownershipService;

    public GetMedicineLogsByMedicineIdQueryValidator(IEndSicknessContext db, IResourceOwnershipService ownershipService)
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
