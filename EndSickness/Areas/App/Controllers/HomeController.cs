using Microsoft.AspNetCore.Mvc;
using EndSickness.Services;
using EndSickness.Models.ViewModels;

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
}

