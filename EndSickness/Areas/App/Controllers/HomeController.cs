using Microsoft.AspNetCore.Mvc;
using EndSickness.Services;
using EndSickness.Shared.Medicines.Queries.GetDosages;
using EndSickness.Shared.Medicines.Queries.GetMedicines;
using EndSickness.Models.ViewModels;

namespace EndSickness.Area.App.Controllers;

[Area("App")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICurrentUserService _currentUserService;

    public HomeController(ILogger<HomeController> logger, ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<IActionResult> Index()
    {
        List<HttpRequestMessage> requests = new()
        {
            new HttpRequestMessage(HttpMethod.Get, $"v1/medicines/dosages"),
            new HttpRequestMessage(HttpMethod.Get, $"v1/medicines"),
        };
        try
        {
            var dosagesClient = HttpContext.RequestServices.GetRequiredService<IEndCrudClient<GetDosagesVm>>();
            var dosagesVm = await dosagesClient.Send(requests[0]);
            var medicinesClient = HttpContext.RequestServices.GetRequiredService<IEndCrudClient<GetMedicinesVm>>();
            var medicinesVm = await medicinesClient.Send(requests[1]);

            IndexViewModel indexViewModel = new(dosagesVm, medicinesVm, _currentUserService.Username);
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
    public async Task<IActionResult> CreateMedicine()
    {
        CreateMedicineViewModel vm = new();
        return View(model: vm);
    }

    //[HttpPost]
    //[Route("/Medicine/Create")]
    //public async Task<IActionResult> CreateMedicine(CreateMedicineViewModel vm)
    //{
    //    var command = new HttpRequestMessage(HttpMethod.Post, $"v1/medicines");
        
    //}
}

