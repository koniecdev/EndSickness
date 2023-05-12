using EndSickness.Domain.Entities;
using EndSickness.Shared.MedicineLogs.Commands.UpdateMedicineLog;

namespace EndSickness.Application.MedicineLogs.Commands.UpdateMedicineLog;

public class UpdateMedicineLogCommandHandler : IRequestHandler<UpdateMedicineLogCommand>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;
    private readonly IResourceOwnershipService _ownershipService;

    public UpdateMedicineLogCommandHandler(IEndSicknessContext db, IMapper mapper, IResourceOwnershipService ownershipService)
    {
        _db = db;
        _mapper = mapper;
        _ownershipService = ownershipService;
    }

    public async Task Handle(UpdateMedicineLogCommand request, CancellationToken cancellationToken)
    {
        var medicineLogToUpdateFromDb = await RetrieveRequestedMedicineLog(request.Id, cancellationToken);
        await CheckGivenMedicineId(request.MedicineId, cancellationToken);

        _mapper.Map(request, medicineLogToUpdateFromDb);
        await _db.SaveChangesAsync(cancellationToken);
    }

    private async Task<MedicineLog> RetrieveRequestedMedicineLog(int medicineLogId, CancellationToken cancellationToken)
    {
        var medicineLogToUpdateFromDb = await _db.MedicineLogs.SingleOrDefaultAsync(m => m.StatusId != 0 && m.Id == medicineLogId, cancellationToken)
            ?? throw new ResourceNotFoundException();
        _ownershipService.CheckOwnership(medicineLogToUpdateFromDb.OwnerId);
        return medicineLogToUpdateFromDb;
    }

    private async Task CheckGivenMedicineId(int? medicineId, CancellationToken cancellationToken)
    {
        if (medicineId is not null)
        {
            var medicineThatWeWantAssignToUpdatingMedicineLogFromDb =
            await _db.Medicines
            .SingleOrDefaultAsync(m => m.Id == medicineId && m.StatusId != 0, cancellationToken)
                ?? throw new ResourceNotFoundException();
            _ownershipService.CheckOwnership(medicineThatWeWantAssignToUpdatingMedicineLogFromDb.OwnerId);
        }
    }
}
