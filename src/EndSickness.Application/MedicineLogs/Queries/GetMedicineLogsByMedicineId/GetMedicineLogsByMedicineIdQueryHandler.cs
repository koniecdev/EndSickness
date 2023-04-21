using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

namespace EndSickness.Application.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

public class GetMedicineLogsByMedicineIdQueryHandler : IRequestHandler<GetMedicineLogsByMedicineIdQuery, GetMedicineLogsByMedicineIdVm>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;

    public GetMedicineLogsByMedicineIdQueryHandler(IEndSicknessContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<GetMedicineLogsByMedicineIdVm> Handle(GetMedicineLogsByMedicineIdQuery request, CancellationToken cancellationToken)
    {
        var medicineLogs = await _db.MedicineLogs.Where(m => m.MedicineId == request.MedicineId && m.StatusId != 0)
            .ProjectTo<GetMedicineLogsByMedicineIdDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        return new GetMedicineLogsByMedicineIdVm(request.MedicineId, medicineLogs);
    }
}
