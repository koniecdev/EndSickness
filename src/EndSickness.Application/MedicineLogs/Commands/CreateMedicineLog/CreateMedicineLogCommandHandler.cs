using EndSickness.Domain.Entities;
using EndSickness.Shared.MedicineLogs.Commands.CreateMedicineLog;

namespace EndSickness.Application.MedicineLogs.Commands.CreateMedicineLog;

public class CreateMedicineLogCommandHandler : IRequestHandler<CreateMedicineLogCommand, int>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;
    private readonly IResourceOwnershipService _ownershipService;

    public CreateMedicineLogCommandHandler(IEndSicknessContext db, IMapper mapper, IResourceOwnershipService ownershipService)
    {
        _db = db;
        _mapper = mapper;
        _ownershipService = ownershipService;
    }

    public async Task<int> Handle(CreateMedicineLogCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<MedicineLog>(request);
        var medFromDb = await _db.Medicines.SingleOrDefaultAsync(m => m.Id == request.MedicineId && m.StatusId != 0, cancellationToken)
            ?? throw new ResourceNotFoundException(nameof(Medicine), request.MedicineId);
        _ownershipService.CheckOwnership(medFromDb.OwnerId);
        _db.MedicineLogs.Add(mapped);
        return await _db.SaveChangesAsync(cancellationToken);
    }
}
