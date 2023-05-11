using EndSickness.Domain.Entities;
using EndSickness.Shared.Medicines.Commands.DeleteMedicine;
using System.Threading;

namespace EndSickness.Application.Medicines.Commands.DeleteMedicine;

public class DeleteMedicineCommandHandler : IRequestHandler<DeleteMedicineCommand>
{
    private readonly IEndSicknessContext _db;
    private readonly IResourceOwnershipService _ownershipService;

    public DeleteMedicineCommandHandler(IEndSicknessContext db, IResourceOwnershipService ownershipService)
    {
        _db = db;
        _ownershipService = ownershipService;
    }
    public async Task Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
    {
        var fromDb = await _db.Medicines.SingleOrDefaultAsync(m => m.StatusId != 0 && m.Id == request.Id, cancellationToken)
            ?? throw new ResourceNotFoundException();
        _ownershipService.CheckOwnership(fromDb.OwnerId);
        await DeleteMedicineAsync(fromDb, cancellationToken);
    }

    private async Task DeleteMedicineAsync(Medicine fromDb, CancellationToken cancellationToken)
    {
        _db.Medicines.Remove(fromDb);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
