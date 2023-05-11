using EndSickness.Domain.Entities;
using EndSickness.Shared.Medicines.Queries.GetDosageById;

namespace EndSickness.Application.Medicines.Queries.GetDosageById;

public class GetDosageByIdQueryHandler : IRequestHandler<GetDosageByIdQuery, GetDosageByIdVm>
{
    private readonly IEndSicknessContext _db;
    private readonly ICalculateNeariestDosageService _calculateNeariestDosageService;
    private readonly IResourceOwnershipService _ownershipService;

    public GetDosageByIdQueryHandler(IEndSicknessContext db, ICalculateNeariestDosageService calculateNeariestDosageService, IResourceOwnershipService ownershipService)
    {
        _db = db;
        _calculateNeariestDosageService = calculateNeariestDosageService;
        _ownershipService = ownershipService;
    }

    public async Task<GetDosageByIdVm> Handle(GetDosageByIdQuery request, CancellationToken cancellationToken)
    {
        var medicine =  await _db.Medicines.Where(m => m.StatusId != 0 && m.Id == request.MedicineId).SingleOrDefaultAsync(cancellationToken)
            ?? throw new ResourceNotFoundException();
        _ownershipService.CheckOwnership(medicine.OwnerId);

        var medicineLogs = await _db.MedicineLogs
            .OrderByDescending(m=>m.LastlyTaken)
            .Where(m => m.MedicineId == medicine.Id && m.StatusId != 0)
            .ToListAsync(cancellationToken);

        if(medicineLogs.Count == 0)
        {
            throw new EmptyResultException(); //Exception handling middleware will translate this exception to 204 No Content result.
        }

        return VmFactory(medicine, medicineLogs);
    }

    private GetDosageByIdVm VmFactory(Medicine medicine, ICollection<MedicineLog> medicineLogs)
    {
        DateTime vmLastDose = medicineLogs.First().LastlyTaken;
        DateTime vmNextDose = _calculateNeariestDosageService.Calculate(vmLastDose, medicine, medicineLogs);

        return new GetDosageByIdVm()
        {
            MedicineId = medicine.Id,
            MedicineName = medicine.Name,
            LastDose = vmLastDose,
            NextDose = vmNextDose,
        };
    }
}
