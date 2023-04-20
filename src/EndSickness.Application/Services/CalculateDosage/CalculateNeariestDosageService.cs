using EndSickness.Domain.Entities;

namespace EndSickness.Application.Services.CalculateDosage;

public class CalculateNeariestDosageService : ICalculateNeariestDosageService
{
    public DateTime Calculate(DateTime lastDose, Medicine operatingMedicine, ICollection<MedicineLog> operatingMedicineLogs)
    {
        DateTime? vmNextDose;
        var previousDayDoses = operatingMedicineLogs.OrderBy(m => m.LastlyTaken).Where(m => m.LastlyTaken >= lastDose - TimeSpan.FromHours(24)).ToList();
        var theoreticalNextDoseIfWeDontIncludeDailyLimit = previousDayDoses.Last().LastlyTaken + TimeSpan.FromHours(operatingMedicine.HourlyCooldown);

        if (previousDayDoses.Count == operatingMedicine.MaxDailyAmount)
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

        return vmNextDose.Value;
    }
}
