using EndSickness.Application.Common.Validation;
using EndSickness.Shared.Medicines.Queries.GetMedicineById;

namespace EndSickness.Application.Medicines.Queries.GetMedicineById;

public class GetMedicineByIdQueryValidator : AbstractValidator<GetMedicineByIdQuery>
{
    private readonly IEndSicknessContext _db;
    private readonly IResourceOwnershipService _ownershipService;

    public GetMedicineByIdQueryValidator(IEndSicknessContext db ,IResourceOwnershipService ownershipService)
    {
        RuleFor(m => m.Id).NotEmpty().GreaterThan(0).LessThan(int.MaxValue)
            .MustAsync(async (medicineId, cancellationToken) => await ValidateOwnershipAsync(medicineId, cancellationToken));
        _db = db;
        _ownershipService = ownershipService;
    }

    private async Task<bool> ValidateOwnershipAsync(int medicineId, CancellationToken cancellationToken)
    {
        OwnershipValidationStrategy validationStrategy = new MedicineOwnershipValidationStrategy(_db, _ownershipService);
        return await validationStrategy.Validate(medicineId, cancellationToken);
    }
}
