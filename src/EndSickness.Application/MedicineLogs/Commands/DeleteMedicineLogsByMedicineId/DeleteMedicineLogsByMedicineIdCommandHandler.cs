using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;
using MediatR;

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
        var requestedMedicineFromDb = await _db.Medicines.SingleOrDefaultAsync(m => m.Id == request.MedicineId && m.StatusId != 0, cancellationToken)
            ?? throw new ResourceNotFoundException();
        _ownershipService.CheckOwnership(requestedMedicineFromDb.OwnerId);

        await RemoveMedicineLogsAssociatedToGivenMedicine(requestedMedicineFromDb.Id, cancellationToken);
    }

    public async Task RemoveMedicineLogsAssociatedToGivenMedicine(int medicineId, CancellationToken cancellationToken)
    {
        var medicineLogsOfRequestedMedicineList = await _db.MedicineLogs.Where(m => m.MedicineId == medicineId && m.StatusId != 0).ToListAsync(cancellationToken);
        _db.MedicineLogs.RemoveRange(medicineLogsOfRequestedMedicineList);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
