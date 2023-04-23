using EndSickness.Shared.Medicines.Queries.GetMedicineById;

namespace EndSickness.Application.Medicines.Queries.GetMedicineById;

public class GetMedicineByIdQueryHandler : IRequestHandler<GetMedicineByIdQuery, GetMedicineByIdVm>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;

    public GetMedicineByIdQueryHandler(IEndSicknessContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<GetMedicineByIdVm> Handle(GetMedicineByIdQuery request, CancellationToken cancellationToken)
    {
        var fromDb = await _db.Medicines.Where(m => m.StatusId != 0 && m.Id == request.Id).SingleOrDefaultAsync(cancellationToken) ?? throw new ResourceNotFoundException();
        return _mapper.Map<GetMedicineByIdVm>(fromDb);
    }
}
