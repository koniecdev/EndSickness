using EndSickness.Shared.Dtos;

namespace EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogs;

public record GetMedicineLogsVm(ICollection<MedicineLogDto> MedicineLogs);