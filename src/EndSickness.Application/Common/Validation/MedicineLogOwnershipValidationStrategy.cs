using EndSickness.Domain.Common;

namespace EndSickness.Application.Common.Validation;

public class MedicineLogOwnershipValidationStrategy : OwnershipValidationStrategy
{
    public MedicineLogOwnershipValidationStrategy(IEndSicknessContext _db, IResourceOwnershipService _ownershipService) : base(_db, _ownershipService)
    {
    }

    protected override async Task<AuditableEntity> GetEntityFromDb(int resourceId, CancellationToken cancellationToken)
    {
        return await Db.MedicineLogs.Where(m => m.StatusId != 0 && m.Id == resourceId).SingleOrDefaultAsync(cancellationToken)
            ?? throw new ResourceNotFoundException();
    }
}
