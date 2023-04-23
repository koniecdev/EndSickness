using EndSickness.Domain.Common;

namespace EndSickness.Application.Common.Validation;

public abstract class OwnershipValidationStrategy
{
    public OwnershipValidationStrategy(IEndSicknessContext _db, IResourceOwnershipService ownershipService)
    {
        Db = _db;
        OwnershipService = ownershipService;
    }

    protected IEndSicknessContext Db { get; private set; }
    protected IResourceOwnershipService OwnershipService { get; private set; }

    public async Task<bool> Validate(int resourceId, CancellationToken cancellationToken)
    {
        AuditableEntity entity = await GetEntityFromDb(resourceId, cancellationToken);
        OwnershipService.CheckOwnership(entity.OwnerId);
        return true;
    }

    protected abstract Task<AuditableEntity> GetEntityFromDb(int resourceId, CancellationToken cancellationToken);
}
