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
        var fromDb = await _db.MedicineLogs.SingleOrDefaultAsync(m => m.StatusId != 0 && m.Id == request.Id, cancellationToken)
            ?? throw new ResourceNotFoundException();
        if (request.MedicineId != null)
        {
            var nestedMedicine = await _db.Medicines.SingleOrDefaultAsync(m => m.StatusId != 0 && m.Id == request.MedicineId, cancellationToken)
                ?? throw new ResourceNotFoundException(nameof(Medicine), request.MedicineId.Value);
            _ownershipService.CheckOwnership(nestedMedicine.OwnerId);
        }
        _ownershipService.CheckOwnership(fromDb.OwnerId);
        _mapper.Map(request, fromDb);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
