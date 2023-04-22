using EndSickness.Domain.Common;

namespace EndSickness.Application.Common.Validation;

public class MedicineOwnershipValidationStrategy : OwnershipValidationStrategy
{
    public MedicineOwnershipValidationStrategy(IEndSicknessContext _db, IResourceOwnershipService ownershipService) : base(_db, ownershipService)
    {
    }

    protected override async Task<AuditableEntity> GetEntityFromDb(int resourceId, CancellationToken cancellationToken)
    {
        return await Db.Medicines.Where(m => m.StatusId != 0 && m.Id == resourceId).SingleOrDefaultAsync(cancellationToken)
            ?? throw new ResourceNotFoundException();
    }
}
