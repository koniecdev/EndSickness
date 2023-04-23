using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogById;

namespace EndSickness.Application.MedicineLogs.Queries.GetMedicineLogById;

public class GetMedicineLogByIdQueryValidator : AbstractValidator<GetMedicineLogByIdQuery>
{
    private readonly IEndSicknessContext _db;
    private readonly IResourceOwnershipService _ownershipService;

    public GetMedicineLogByIdQueryValidator(IEndSicknessContext db, IResourceOwnershipService ownershipService)
    {
        _db = db;
        _ownershipService = ownershipService;
        RuleFor(m => m.Id).NotEmpty().GreaterThan(0).LessThan(int.MaxValue)
            .MustAsync(async (id, cancellationToken) => await ValidateOwnershipAsync(id, cancellationToken));
    }

    private async Task<bool> ValidateOwnershipAsync(int id, CancellationToken cancellationToken)
    {
        var strategy = new MedicineLogOwnershipValidationStrategy(_db, _ownershipService);
        return await strategy.Validate(id, cancellationToken);
    }
}
