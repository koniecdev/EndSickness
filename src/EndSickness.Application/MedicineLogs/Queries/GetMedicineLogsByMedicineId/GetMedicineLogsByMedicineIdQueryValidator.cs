using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

namespace EndSickness.Application.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

public class GetMedicineLogsByMedicineIdQueryValidator : AbstractValidator<GetMedicineLogsByMedicineIdQuery>
{
    private readonly IEndSicknessContext _db;
    private readonly IResourceOwnershipService _ownershipService;

    public GetMedicineLogsByMedicineIdQueryValidator(IEndSicknessContext db, IResourceOwnershipService ownershipService)
    {
        _db = db;
        _ownershipService = ownershipService;
        RuleFor(m => m.MedicineId).NotEmpty().GreaterThan(0).LessThan(int.MaxValue)
            .MustAsync(async (medicineId, cancellationToken) => await ValidateOwnershipAsync(medicineId, cancellationToken));
    }

    private async Task<bool> ValidateOwnershipAsync(int medicineId, CancellationToken cancellationToken)
    {
        OwnershipValidationStrategy strategy = new MedicineOwnershipValidationStrategy(_db, _ownershipService);
        return await strategy.Validate(medicineId, cancellationToken);
    }
}
