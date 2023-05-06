using Microsoft.AspNetCore.Mvc;

namespace EndSickness.ViewComponents.ViewComponents;

public class UsernameViewComponents : ViewComponent
{
    public UsernameViewComponents()
    {
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        string username = "";
        await Task.Run(() =>
        {
            var usernameClaim = HttpContext.User.Claims.FirstOrDefault(m => m.Type == "Name");
            if(usernameClaim is not null)
            {
                username = usernameClaim.Value;
            }
        });
        return View(model: username);
    }
}