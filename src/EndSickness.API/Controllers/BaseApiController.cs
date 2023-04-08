using Microsoft.AspNetCore.Cors;

namespace EndSickness.API.Controllers;

[ApiController]
[EnableCors("AllowedPolicies")]
public class BaseApiController : ControllerBase
{
    protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>()!;
}
