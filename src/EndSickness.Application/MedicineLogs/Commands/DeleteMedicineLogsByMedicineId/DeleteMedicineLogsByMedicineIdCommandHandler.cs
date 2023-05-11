using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;

namespace EndSickness.Application.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;

public class DeleteMedicineLogsByMedicineIdCommandHandler : IRequestHandler<DeleteMedicineLogsByMedicineIdCommand>
{
    private readonly IEndSicknessContext _db;
    private readonly IResourceOwnershipService _ownershipService;

    public DeleteMedicineLogsByMedicineIdCommandHandler(IEndSicknessContext db, IResourceOwnershipService ownershipService)
    {
        _db = db;
        _ownershipService = ownershipService;
    }
    public async Task Handle(DeleteMedicineLogsByMedicineIdCommand request, CancellationToken cancellationToken)
    {
        var medicine = await _db.Medicines.SingleOrDefaultAsync(m => m.Id == request.MedicineId && m.StatusId != 0, cancellationToken)
            ?? throw new ResourceNotFoundException();
        _ownershipService.CheckOwnership(medicine.OwnerId);

        var fromDb = await _db.MedicineLogs.Where(m => m.MedicineId == request.MedicineId && m.StatusId != 0).ToListAsync(cancellationToken);
        _db.MedicineLogs.RemoveRange(fromDb);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
