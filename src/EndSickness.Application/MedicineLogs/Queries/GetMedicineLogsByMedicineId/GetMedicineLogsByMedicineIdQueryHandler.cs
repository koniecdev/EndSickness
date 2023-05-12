using EndSickness.Domain.Entities;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogsByMedicineId;
using MediatR;

namespace EndSickness.Application.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

public class GetMedicineLogsByMedicineIdQueryHandler : IRequestHandler<GetMedicineLogsByMedicineIdQuery, GetMedicineLogsByMedicineIdVm>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;
    private readonly IResourceOwnershipService _ownershipService;

    public GetMedicineLogsByMedicineIdQueryHandler(IEndSicknessContext db, IMapper mapper, IResourceOwnershipService ownershipService)
    {
        _db = db;
        _mapper = mapper;
        _ownershipService = ownershipService;
    }

    public async Task<GetMedicineLogsByMedicineIdVm> Handle(GetMedicineLogsByMedicineIdQuery request, CancellationToken cancellationToken)
    {
        var medicineLogs = await RetrieveRequestedMedicineLogsAsync(request.MedicineId, cancellationToken);
        var mapped = _mapper.Map<List<GetMedicineLogsByMedicineIdDto>>(medicineLogs);
        return new GetMedicineLogsByMedicineIdVm(request.MedicineId, mapped);
    }

    private async Task<List<MedicineLog>> RetrieveRequestedMedicineLogsAsync(int medicineId, CancellationToken cancellationToken)
    {
        var medicineLogs = await _db.MedicineLogs.Where(m => m.MedicineId == medicineId && m.StatusId != 0).ToListAsync(cancellationToken);
        if (medicineLogs.Count == 0)
        {
            throw new EmptyResultException();
        }
        _ownershipService.CheckOwnership(medicineLogs.First().OwnerId);
        return medicineLogs;
    }
}
