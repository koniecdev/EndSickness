namespace EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogs;

public record GetMedicineLogsVm
{
    public GetMedicineLogsVm()
    {
        MedicineLogs = new List<GetMedicineLogsDto>();
    }
    public GetMedicineLogsVm(ICollection<GetMedicineLogsDto> medicines)
    {
        MedicineLogs = medicines;
    }
    public ICollection<GetMedicineLogsDto> MedicineLogs { get; init; }
}