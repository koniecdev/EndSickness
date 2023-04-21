using EndSickness.Shared.Medicines.Queries.GetDosageById;

namespace EndSickness.Application.Medicines.Queries.GetDosageById;

public class GetDosageByIdQueryHandler : IRequestHandler<GetDosageByIdQuery, GetDosageByIdVm>
{
    private readonly IEndSicknessContext _db;
    private readonly ICalculateNeariestDosageService _calculateNeariestDosageService;

    public GetDosageByIdQueryHandler(IEndSicknessContext db, ICalculateNeariestDosageService calculateNeariestDosageService)
    {
        _db = db;
        _calculateNeariestDosageService = calculateNeariestDosageService;
    }

    public async Task<GetDosageByIdVm> Handle(GetDosageByIdQuery request, CancellationToken cancellationToken)
    {
        var medicine = await _db.Medicines.SingleAsync(m => m.Id == request.MedicineId && m.StatusId != 0, cancellationToken);
        var medicineLogs = await _db.MedicineLogs
            .OrderByDescending(m=>m.LastlyTaken)
            .Where(m => m.MedicineId == medicine.Id && m.StatusId != 0)
            .ToListAsync(cancellationToken);

        if(medicineLogs.Count == 0)
        {
            throw new EmptyResultException();
        }

        DateTime vmLastDose = medicineLogs.First().LastlyTaken;
        DateTime vmNextDose = _calculateNeariestDosageService.Calculate(vmLastDose, medicine, medicineLogs);

        return new GetDosageByIdVm()
        {
            MedicineId = medicine.Id,
            MedicineName = medicine.Name,
            LastDose = TimeOnly.FromDateTime(vmLastDose),
            NextDose = TimeOnly.FromDateTime(vmNextDose),
        };
    }
}
