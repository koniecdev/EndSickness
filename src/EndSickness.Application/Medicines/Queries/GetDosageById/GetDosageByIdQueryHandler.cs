using EndSickness.Shared.Medicines.Queries.GetDosageById;
using System.Collections.Immutable;

namespace EndSickness.Application.Medicines.Queries.GetDosageById;

public class GetDosageByIdQueryHandler : IRequestHandler<GetDosageByIdQuery, GetDosageByIdVm>
{
    private readonly IEndSicknessContext _db;

    public GetDosageByIdQueryHandler(IEndSicknessContext db)
    {
        _db = db;
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

        DateTime? vmNextDose;

        DateTime vmLastDose = medicineLogs.First().LastlyTaken;
        var previousDayDoses = medicineLogs.OrderBy(m=>m.LastlyTaken).Where(m => m.LastlyTaken >= vmLastDose - TimeSpan.FromHours(24)).ToList();
        var theoreticalNextDoseIfWeDontIncludeDailyLimit = previousDayDoses.Last().LastlyTaken + TimeSpan.FromHours(medicine.HourlyCooldown);

        if(previousDayDoses.Count == medicine.MaxDailyAmount)
        {
            var nextDoseWithDailyMax = previousDayDoses.First().LastlyTaken + TimeSpan.FromHours(24);
            if (nextDoseWithDailyMax < theoreticalNextDoseIfWeDontIncludeDailyLimit)
            {
                vmNextDose = theoreticalNextDoseIfWeDontIncludeDailyLimit;
            }
            else
            {
                vmNextDose = nextDoseWithDailyMax;
            }
        }
        else //overdose is not possible
        {
            vmNextDose = theoreticalNextDoseIfWeDontIncludeDailyLimit;
        }
        return new GetDosageByIdVm()
        {
            MedicineId = medicine.Id,
            MedicineName = medicine.Name,
            LastDose = TimeOnly.FromDateTime(vmLastDose),
            NextDose = TimeOnly.FromDateTime(vmNextDose.Value),
            TakeUntil = DateOnly.FromDateTime(previousDayDoses.First().LastlyTaken + TimeSpan.FromDays(medicine.MaxDaysOfTreatment))
        };
    }
}
