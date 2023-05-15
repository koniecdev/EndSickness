using EndSickness.Domain.Entities;
using EndSickness.Shared.MedicineLogs.Commands.CreateMedicineLog;
using MediatR;
using System.Linq;
using System.Threading;

namespace EndSickness.Application.MedicineLogs.Commands.CreateMedicineLog;

public class CreateMedicineLogCommandHandler : IRequestHandler<CreateMedicineLogCommand, int>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;
    private readonly IResourceOwnershipService _ownershipService;
    private readonly IPreventOverdosingService _preventOverdosingService;

    public CreateMedicineLogCommandHandler(IEndSicknessContext db, IMapper mapper, IResourceOwnershipService ownershipService, IPreventOverdosingService preventOverdosingService)
    {
        _db = db;
        _mapper = mapper;
        _ownershipService = ownershipService;
        _preventOverdosingService = preventOverdosingService;
    }

    public async Task<int> Handle(CreateMedicineLogCommand request, CancellationToken cancellationToken)
    {
        var mappedMedicineLog = _mapper.Map<MedicineLog>(request);
        await CheckForMedicine(mappedMedicineLog, cancellationToken);
        await _preventOverdosingService.HandleAsync(request.MedicineId, request.LastlyTaken, cancellationToken);
        await AddNewMedicineLogToDbAsync(mappedMedicineLog, cancellationToken);
        return mappedMedicineLog.Id;
    }

    private async Task CheckForMedicine(MedicineLog medicineLog, CancellationToken cancellationToken)
    {
        var operatingMedicine = await _db.Medicines.SingleOrDefaultAsync(m => m.Id == medicineLog.MedicineId, cancellationToken)
            ?? throw new ResourceNotFoundException();
        _ownershipService.CheckOwnership(operatingMedicine.OwnerId);
    }

    private async Task AddNewMedicineLogToDbAsync(MedicineLog medicineLog, CancellationToken cancellationToken)
    {
        _db.MedicineLogs.Add(medicineLog);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
