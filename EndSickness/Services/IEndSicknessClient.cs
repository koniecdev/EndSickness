using EndSickness.Shared.Dtos;
using EndSickness.Shared.MedicineLogs.Commands.CreateMedicineLog;
using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLog;
using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;
using EndSickness.Shared.MedicineLogs.Commands.UpdateMedicineLog;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogById;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogs;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogsByMedicineId;
using EndSickness.Shared.Medicines.Commands.CreateMedicine;
using EndSickness.Shared.Medicines.Commands.DeleteMedicine;
using EndSickness.Shared.Medicines.Commands.UpdateMedicine;
using EndSickness.Shared.Medicines.Queries.GetDosageById;
using EndSickness.Shared.Medicines.Queries.GetDosages;
using EndSickness.Shared.Medicines.Queries.GetMedicineById;
using EndSickness.Shared.Medicines.Queries.GetMedicines;

namespace EndSickness.Services
{
    public interface IEndSicknessClient
    {
        public Task<int> CreateMedicine(CreateMedicineCommand command);
        public Task UpdateMedicine(UpdateMedicineCommand command);
        public Task DeleteMedicine(DeleteMedicineCommand command);
        public Task<GetMedicinesVm> GetAllMedicines(); 
        public Task<MedicineDto> GetMedicineById(GetMedicineByIdQuery query); 
        public Task<GetDosagesVm> GetDosages();
        public Task<DosageDto> GetDosageById(GetDosageByIdQuery query);

        public Task<int> CreateMedicineLog(CreateMedicineLogCommand command);
        public Task UpdateMedicineLog(UpdateMedicineLogCommand command);
        public Task DeleteMedicineLog(DeleteMedicineLogCommand command);
        public Task DeleteMedicineLogsByMedicineId(DeleteMedicineLogsByMedicineIdCommand command);
        public Task<GetMedicineLogsVm> GetAllMedicineLogs();
        public Task<MedicineLogDto> GetMedicineLogById(GetMedicineLogByIdQuery query);
        public Task<GetMedicineLogsByMedicineIdVm> GetMedicineLogsByMedicineId(GetMedicineLogsByMedicineIdQuery query);
    }
}