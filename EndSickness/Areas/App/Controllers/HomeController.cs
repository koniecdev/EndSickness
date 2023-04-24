using Microsoft.AspNetCore.Mvc;
using EndSickness.Services;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using EndSickness.Shared.Medicines.Queries.GetDosages;
using Newtonsoft.Json;
using IdentityModel.Client;
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

}

