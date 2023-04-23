using EndSickness.Application.Common.Validation;
using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLog;

namespace EndSickness.Application.MedicineLogs.Commands.DeleteMedicineLog;

public class DeleteMedicineLogCommandValidator : AbstractValidator<DeleteMedicineLogCommand>
{
    private readonly IEndSicknessContext _db;
    private readonly IResourceOwnershipService _ownershipService;

    public DeleteMedicineLogCommandValidator(IEndSicknessContext db, IResourceOwnershipService ownershipService)
    {
        _db = db;
        _ownershipService = ownershipService;
        RuleFor(m => m.Id).GreaterThan(0).LessThan(int.MaxValue)
            .MustAsync(async (id, cancellationToken) => await CheckOwnershipAsync(id, cancellationToken))
            .NotEmpty();
    }

    private async Task<bool> CheckOwnershipAsync(int id, CancellationToken cancellationToken)
    {
        OwnershipValidationStrategy strategy = new MedicineLogOwnershipValidationStrategy(_db, _ownershipService);
        return await strategy.Validate(id, cancellationToken);
    }
}
