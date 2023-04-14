using EndSickness.Shared.Medicines.Commands.DeleteMedicine;

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
        var fromDb = await _db.Medicines.Where(m => m.StatusId != 0 && m.Id == request.Id).SingleOrDefaultAsync(cancellationToken)
            ?? throw new ResourceNotFoundException();
        _ownershipService.CheckOwnership(fromDb.OwnerId);
        _db.Medicines.Remove(fromDb);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
