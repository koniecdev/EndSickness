using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;

namespace EndSickness.Application.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;

public class DeleteMedicineLogsByMedicineIdCommandValidator : AbstractValidator<DeleteMedicineLogsByMedicineIdCommand>
{
    private readonly IEndSicknessContext _db;
    private readonly IResourceOwnershipService _ownershipService;

    public DeleteMedicineLogsByMedicineIdCommandValidator(IEndSicknessContext db, IResourceOwnershipService ownershipService)
    {
        _db = db;
        _ownershipService = ownershipService;
        RuleFor(m => m.MedicineId).GreaterThan(0).LessThan(int.MaxValue)
            .MustAsync(async (id, cancellationToken) => await CheckOwnershipAsync(id, cancellationToken))
            .NotEmpty();
    }

    private async Task<bool> CheckOwnershipAsync(int id, CancellationToken cancellationToken)
    {
        OwnershipValidationStrategy strategy = new MedicineOwnershipValidationStrategy(_db, _ownershipService);
        return await strategy.Validate(id, cancellationToken);
    }
}
