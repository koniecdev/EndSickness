using EndSickness.Domain.Entities;
using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLog;

namespace EndSickness.Application.MedicineLogs.Commands.DeleteMedicineLog;

public class DeleteMedicineLogCommandHandler : IRequestHandler<DeleteMedicineLogCommand>
{
    private readonly IEndSicknessContext _db;
    private readonly IResourceOwnershipService _ownershipService;

    public DeleteMedicineLogCommandHandler(IEndSicknessContext db, IResourceOwnershipService ownershipService)
    {
        _db = db;
        _ownershipService = ownershipService;
    }

    public async Task Handle(DeleteMedicineLogCommand request, CancellationToken cancellationToken)
    {
        var requestedMedicineLogFromDb = await _db.MedicineLogs.SingleOrDefaultAsync(m => m.StatusId != 0 && m.Id == request.Id, cancellationToken)
            ?? throw new ResourceNotFoundException();
        _ownershipService.CheckOwnership(requestedMedicineLogFromDb.OwnerId);
        await RemoveMedicineLogFromDbAsync(requestedMedicineLogFromDb, cancellationToken);
    }

    private async Task RemoveMedicineLogFromDbAsync(MedicineLog medicineLog, CancellationToken cancellationToken)
    {
        _db.MedicineLogs.Remove(medicineLog);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
