using EndSickness.Domain.Entities;

namespace EndSickness.Application.Services.CalculateDosage;

public class CalculateNeariestDosageService : ICalculateNeariestDosageService
{
    public DateTime Calculate(DateTime lastDose, Medicine operatingMedicine, ICollection<MedicineLog> operatingMedicineLogs)
    {
        var previousDayDoses = operatingMedicineLogs.OrderBy(m => m.LastlyTaken).Where(m => m.LastlyTaken >= lastDose - TimeSpan.FromHours(24)).ToList();
        DateTime theoreticalNextDoseDateIfWeDontIncludeDailyLimit = previousDayDoses.Last().LastlyTaken + TimeSpan.FromHours(operatingMedicine.HourlyCooldown);

        if (previousDayDoses.Count == operatingMedicine.MaxDailyAmount)
        {
            var nextDoseWithDailyMax = previousDayDoses.First().LastlyTaken + TimeSpan.FromHours(24);
            if (nextDoseWithDailyMax < theoreticalNextDoseDateIfWeDontIncludeDailyLimit)
            {
                return theoreticalNextDoseDateIfWeDontIncludeDailyLimit;
            }
            else
            {
                return nextDoseWithDailyMax;
            }
        }
        return theoreticalNextDoseDateIfWeDontIncludeDailyLimit;
    }
}
