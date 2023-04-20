using EndSickness.Domain.Entities;

namespace EndSickness.Application.Common.Interfaces;

public interface ICalculateNeariestDosageService
{
    DateTime Calculate(DateTime lastDose, Medicine operatingMedicine, ICollection<MedicineLog> operatingMedicineLogs);
}