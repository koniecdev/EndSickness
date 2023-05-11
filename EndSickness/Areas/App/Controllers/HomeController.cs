using Microsoft.AspNetCore.Mvc;
using EndSickness.Services;
using EndSickness.Models.ViewModels;
using EndSickness.Extensions;

namespace EndSickness.Area.App.Controllers;

[Area("App")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICurrentUserService _currentUserService;
    private readonly IEndSicknessClient _client;

    public HomeController(ILogger<HomeController> logger, ICurrentUserService currentUserService, IEndSicknessClient client)
    {
        _logger = logger;
        _currentUserService = currentUserService;
        _client = client;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            IndexViewModel indexViewModel = new(await _client.GetDosages(), await _client.GetAllMedicines(), _currentUserService.Username);
            return View(model: indexViewModel);
        }
        catch (UnauthorizedAccessException)
        {
            return RedirectToAction(nameof(Logout));
        }
    }

    public IActionResult Logout()
    {
        return SignOut("Cookies", "oidc");
    }

    [HttpGet]
    [Route("/Medicine/Create")]
    public IActionResult CreateMedicine()
    {
        CreateMedicineViewModel vm = new();
        return View(model: vm);
    }

    [HttpPost]
    [Route("/Medicine/Create")]
    public async Task<IActionResult> CreateMedicine(CreateMedicineViewModel vm)
    {
        var result = await _client.CreateMedicine(vm.ToCommand());
        return result > 0 ? RedirectToAction(nameof(Index)) : throw new Exception("Could not create new medicine");
    }

    [HttpGet]
    [Route("/Medicine/Update/{id}")]
    public async Task<IActionResult> UpdateMedicine(int id)
    {
        var fromApi = await _client.GetMedicineById(new(id));
        UpdateMedicineViewModel vm = new()
        {
            Id = fromApi.Id,
            Name = fromApi.Name,
            HourlyCooldown = fromApi.HourlyCooldown,
            MaxDailyAmount = fromApi.MaxDailyAmount,
            MaxDaysOfTreatment = fromApi.MaxDaysOfTreatment
        };
        return View(model: vm);
    }

    [HttpPost]
    [Route("/Medicine/Update/{id}")]
    public async Task<IActionResult> UpdateMedicine(UpdateMedicineViewModel vm)
    {
        await _client.UpdateMedicine(vm.ToCommand());
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Route("/Medicine/Delete/{id}")]
    public async Task<IActionResult> DeleteMedicine(int id)
    {
        await _client.DeleteMedicine(new(id));
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Route("/Dosages/Delete/{medicineId}")]
    public async Task<IActionResult> DeleteMedicineLogs(int medicineId)
    {
        await _client.DeleteMedicineLogsByMedicineId(new(medicineId));
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Route("/Dosage/Delete/{id}")]
    public async Task<IActionResult> DeleteMedicineLog(int id)
    {
        await _client.DeleteMedicineLog(new(id));
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    [Route("/Dosage/Note")]
    public async Task<IActionResult> CreateMedicineLog()
    {
        var medicinesVm = await _client.GetAllMedicines();
        CreateMedicineLogViewModel vm = new() {
            Medicines = medicinesVm.Medicines.ToSelectListItem(1)
        };
        return View(model: vm);
    }
    
    [HttpPost]
    [Route("/Dosage/Note")]
    public async Task<IActionResult> CreateMedicineLog(CreateMedicineLogViewModel vm)
    {
        try
        {
            _ = await _client.CreateMedicineLog(vm.ToCommand());
            return RedirectToAction(nameof(Index));
        }
        catch(Exception ex)
        {
            var medicinesList = await _client.GetAllMedicines();
            vm.Medicines = medicinesList.Medicines.ToSelectListItem(1);
            ViewBag.Errors = ex.Message;
            return View(vm);
        }
    }

    [HttpGet]
    [Route("/Medicine/{id}/Dosages")]
    public async Task<IActionResult> MedicineDosages(int id)
    {
        var medicineLogList = await _client.GetMedicineLogsByMedicineId(new(id));
        var medicineLogListFormetted = medicineLogList with { MedicineLogs = medicineLogList.MedicineLogs.OrderByDescending(m=>m.LastlyTaken).ToList()};
        var medicine = await _client.GetMedicineById(new(id));
        var medicineList = await _client.GetAllMedicines();
        return View(new MedicineDosagesViewModel(medicineLogListFormetted, medicineList, medicine));
    }
}

