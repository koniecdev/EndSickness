using EndSickness.Shared.Dtos;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogById;

namespace EndSickness.Application.MedicineLogs.Queries.GetMedicineLogById;

public class GetMedicineLogByIdQueryHandler : IRequestHandler<GetMedicineLogByIdQuery, MedicineLogDto>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;
    private readonly IResourceOwnershipService _ownershipService;

    public GetMedicineLogByIdQueryHandler(IEndSicknessContext db, IMapper mapper, IResourceOwnershipService ownershipService)
    {
        _db = db;
        _mapper = mapper;
        _ownershipService = ownershipService;
    }

    public async Task<MedicineLogDto> Handle(GetMedicineLogByIdQuery request, CancellationToken cancellationToken)
    {
        var medicineLogFromDb = await _db.MedicineLogs.Include(m => m.Medicine)
            .SingleOrDefaultAsync(m => m.StatusId != 0 && m.Id == request.Id, cancellationToken)
            ?? throw new ResourceNotFoundException();
        _ownershipService.CheckOwnership(medicineLogFromDb.OwnerId);

        return _mapper.Map<MedicineLogDto>(medicineLogFromDb);
    }
}
