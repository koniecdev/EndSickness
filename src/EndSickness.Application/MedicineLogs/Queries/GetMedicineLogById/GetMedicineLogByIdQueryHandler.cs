using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogById;

namespace EndSickness.Application.MedicineLogs.Queries.GetMedicineLogById;

public class GetMedicineLogByIdQueryHandler : IRequestHandler<GetMedicineLogByIdQuery, GetMedicineLogByIdVm>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;

    public GetMedicineLogByIdQueryHandler(IEndSicknessContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<GetMedicineLogByIdVm> Handle(GetMedicineLogByIdQuery request, CancellationToken cancellationToken)
    {
        var fromDb = await _db.MedicineLogs.Include(m => m.Medicine)
            .SingleAsync(m => m.StatusId != 0 && m.Id == request.Id, cancellationToken);
        var mapped = _mapper.Map<GetMedicineLogByIdVm>(fromDb);
        return mapped;
    }
}
