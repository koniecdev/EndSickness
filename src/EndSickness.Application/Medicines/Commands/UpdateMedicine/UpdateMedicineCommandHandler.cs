using EndSickness.Domain.Entities;
using EndSickness.Shared.Medicines.Commands.UpdateMedicine;

namespace EndSickness.Application.Medicines.Commands.UpdateMedicine;

public class UpdateMedicineCommandHandler : IRequestHandler<UpdateMedicineCommand>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;
    private readonly IResourceOwnershipService _ownershipService;

    public UpdateMedicineCommandHandler(IEndSicknessContext db, IMapper mapper, IResourceOwnershipService ownershipService)
    {
        _db = db;
        _mapper = mapper;
        _ownershipService = ownershipService;
    }

    public async Task Handle(UpdateMedicineCommand request, CancellationToken cancellationToken)
    {
        var fromDb = await _db.Medicines.Where(m => m.StatusId != 0 && m.Id == request.Id).SingleAsync(cancellationToken)
            ?? throw new ResourceNotFoundException();
        _ownershipService.CheckOwnership(fromDb.OwnerId);
        fromDb = _mapper.Map(request, fromDb);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
