using EndSickness.Shared.Medicines.Queries.GetMedicineById;

namespace EndSickness.Application.Medicines.Queries.GetMedicineById;

public class GetMedicineByIdQueryHandler : IRequestHandler<GetMedicineByIdQuery, MedicineDto>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;
    private readonly IResourceOwnershipService _ownershipService;

    public GetMedicineByIdQueryHandler(IEndSicknessContext db, IMapper mapper, IResourceOwnershipService ownershipService)
    {
        _db = db;
        _mapper = mapper;
        _ownershipService = ownershipService;
    }

    public async Task<MedicineDto> Handle(GetMedicineByIdQuery request, CancellationToken cancellationToken)
    {
        var fromDb = await _db.Medicines.Where(m => m.StatusId != 0 && m.Id == request.Id).SingleOrDefaultAsync(cancellationToken)
            ?? throw new ResourceNotFoundException();
        _ownershipService.CheckOwnership(fromDb.OwnerId);
        return _mapper.Map<MedicineDto>(fromDb);
    }
}
