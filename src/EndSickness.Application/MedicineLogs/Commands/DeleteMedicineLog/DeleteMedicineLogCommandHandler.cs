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
        var fromDb = await _db.MedicineLogs.SingleOrDefaultAsync(m => m.StatusId != 0 && m.Id == request.Id, cancellationToken)
            ?? throw new ResourceNotFoundException();
        _ownershipService.CheckOwnership(fromDb.OwnerId);
        _db.MedicineLogs.Remove(fromDb);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
