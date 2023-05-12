using EndSickness.Domain.Entities;
using EndSickness.Shared.MedicineLogs.Commands.CreateMedicineLog;

namespace EndSickness.Application.MedicineLogs.Commands.CreateMedicineLog;

public class CreateMedicineLogCommandHandler : IRequestHandler<CreateMedicineLogCommand, int>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;
    private readonly IPreventOverdosingService _preventOverdosingService;

    public CreateMedicineLogCommandHandler(IEndSicknessContext db, IMapper mapper, IPreventOverdosingService preventOverdosingService)
    {
        _db = db;
        _mapper = mapper;
        _preventOverdosingService = preventOverdosingService;
    }

    public async Task<int> Handle(CreateMedicineLogCommand request, CancellationToken cancellationToken)
    {
        var mappedMedicineLog = _mapper.Map<MedicineLog>(request);
        await _preventOverdosingService.HandleAsync(request.MedicineId, request.LastlyTaken, cancellationToken);
        await AddNewMedicineLogToDbAsync(mappedMedicineLog, cancellationToken);
        return mappedMedicineLog.Id;
    }

    private async Task AddNewMedicineLogToDbAsync(MedicineLog medicineLog, CancellationToken cancellationToken)
    {
        _db.MedicineLogs.Add(medicineLog);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
