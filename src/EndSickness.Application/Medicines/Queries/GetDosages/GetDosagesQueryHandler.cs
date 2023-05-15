using EndSickness.Shared.Dtos;
using EndSickness.Shared.Medicines.Queries.GetDosages;

namespace EndSickness.Application.Medicines.Queries.GetDosages;

public class GetDosagesQueryHandler : IRequestHandler<GetDosagesQuery, GetDosagesVm>
{
    private readonly IEndSicknessContext _db;
    private readonly ICurrentUserService _currentUser;
    private readonly ICalculateNeariestDosageService _calculateNeariestDosageService;

    public GetDosagesQueryHandler(IEndSicknessContext db, ICurrentUserService currentUser, ICalculateNeariestDosageService calculateNeariestDosageService)
    {
        _db = db;
        _currentUser = currentUser;
        _calculateNeariestDosageService = calculateNeariestDosageService;
    }

    public async Task<GetDosagesVm> Handle(GetDosagesQuery request, CancellationToken cancellationToken)
    {
        var listOfMedicineIdThatUserTake = await _db.MedicineLogs.Include(m=>m.Medicine)
            .Where(m => m.OwnerId == _currentUser.AppUserId && m.StatusId != 0 && m.Medicine.StatusId != 0)
            .Select(m => m.MedicineId).Distinct().ToListAsync(cancellationToken);
        if(listOfMedicineIdThatUserTake.Count == 0)
        {
            throw new EmptyResultException();
        }
        
        return await VmFactory(listOfMedicineIdThatUserTake, cancellationToken);
    }

    private async Task<GetDosagesVm> VmFactory(ICollection<int> medicineLogsIds, CancellationToken cancellationToken)
    {
        List<DosageDto> dosages = new();
        foreach (var medicineId in medicineLogsIds)
        {
            var medicine = await _db.Medicines.SingleAsync(m => m.Id == medicineId && m.StatusId != 0, cancellationToken);
            var medicineLogs = await _db.MedicineLogs
            .OrderByDescending(m => m.LastlyTaken)
            .Where(m => m.MedicineId == medicine.Id && m.StatusId != 0)
            .ToListAsync(cancellationToken);

            DateTime vmLastDose = medicineLogs.First().LastlyTaken;

            DateTime vmNextDose = _calculateNeariestDosageService.Calculate(vmLastDose, medicine, medicineLogs);

            dosages.Add(new DosageDto()
            {
                MedicineId = medicine.Id,
                MedicineName = medicine.Name,
                LastDose = vmLastDose,
                NextDose = vmNextDose
            });
        }
        return new GetDosagesVm(dosages);
    }
}
