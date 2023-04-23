using EndSickness.Shared.Medicines.Queries.GetDosageById;

namespace EndSickness.Application.Medicines.Queries.GetDosageById;

public class GetDosageByIdQueryValidator : AbstractValidator<GetDosageByIdQuery>
{
    private readonly IEndSicknessContext _db;
    private readonly IResourceOwnershipService _ownershipService;

    public GetDosageByIdQueryValidator(IEndSicknessContext db ,IResourceOwnershipService ownershipService)
    {
        _db = db;
        _ownershipService = ownershipService;
        RuleFor(m => m.MedicineId).NotEmpty().GreaterThan(0).LessThan(int.MaxValue)
            .MustAsync(async (medicineId, cancellationToken) => await ValidateOwnershipAsync(medicineId, cancellationToken));
    }

    private async Task<bool> ValidateOwnershipAsync(int medicineId, CancellationToken cancellationToken)
    {
        OwnershipValidationStrategy validationStrategy = new MedicineOwnershipValidationStrategy(_db, _ownershipService);
        return await validationStrategy.Validate(medicineId, cancellationToken);
    }
}
