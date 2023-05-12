using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogs;

namespace EndSickness.Application.MedicineLogs.Queries.GetMedicineLogs;

public class GetMedicineLogsQueryHandler : IRequestHandler<GetMedicineLogsQuery, GetMedicineLogsVm>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;

    public GetMedicineLogsQueryHandler(IEndSicknessContext db, IMapper mapper, ICurrentUserService currentUser)
    {
        _db = db;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<GetMedicineLogsVm> Handle(GetMedicineLogsQuery request, CancellationToken cancellationToken)
    {
        var medicineLogsFromDbList = await _db.MedicineLogs.Include(m => m.Medicine).Where(m => m.StatusId != 0 && m.OwnerId == _currentUser.AppUserId)
            .ProjectTo<GetMedicineLogsDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        if(medicineLogsFromDbList.Count == 0)
        {
            throw new EmptyResultException();
        }
        return new GetMedicineLogsVm(medicineLogsFromDbList);
    }
}
