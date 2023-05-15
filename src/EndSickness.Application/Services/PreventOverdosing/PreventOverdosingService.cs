namespace EndSickness.Application.Services.CalculateDosage;

public class PreventOverdosingService : IPreventOverdosingService
{
    private readonly IEndSicknessContext _db;

    public PreventOverdosingService(IEndSicknessContext db)
    {
        _db = db;
    }
    public async Task HandleAsync(int medicineId, DateTime newDoseDateTime, CancellationToken cancellationToken)
    {
        var operatingMedicine = await _db.Medicines.SingleOrDefaultAsync(m => m.Id == medicineId && m.StatusId != 0, cancellationToken)
            ?? throw new ResourceNotFoundException();

        var previousMedicineLogsList = await _db.MedicineLogs.Where(m => m.MedicineId == medicineId && m.StatusId != 0).ToListAsync(cancellationToken);

        if (previousMedicineLogsList.Count > 0) // We dont have to check for overdosage if there is no medicine logs from the past
        {
            var latestLog = previousMedicineLogsList.OrderByDescending(m => m.LastlyTaken).First();
            if (latestLog.LastlyTaken + TimeSpan.FromHours(operatingMedicine.HourlyCooldown) <= newDoseDateTime) //Check if the cooldown between new and latest dose is correct
            {
                var logsFromDayBeforeNewDose = previousMedicineLogsList.Where(m => m.LastlyTaken > newDoseDateTime - TimeSpan.FromHours(24)).ToList();
                if (logsFromDayBeforeNewDose.Count >= operatingMedicine.MaxDailyAmount) // Check if max. Daily amount of medicine for 24h is not exceeded
                {
                    throw new OverdoseException();
                }
                return; //Everything correct, no action need to be taken
            }
            throw new OverdoseException();
        }
    }
}
