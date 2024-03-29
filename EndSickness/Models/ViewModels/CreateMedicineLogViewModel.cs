﻿using EndSickness.Shared.MedicineLogs.Commands.CreateMedicineLog;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndSickness.Models.ViewModels;
public class CreateMedicineLogViewModel
{
    public IEnumerable<SelectListItem> Medicines { get; set; } = new List<SelectListItem>();
    public int MedicineId { get; set; }
    public DateTime LastlyTaken { get; set; } = DateTime.Now;
    public CreateMedicineLogCommand ToCommand()
    {
        return new CreateMedicineLogCommand(MedicineId, LastlyTaken);
    }
}
