using Microsoft.AspNetCore.Cors;

namespace EndSickness.API.Controllers;

[ApiController]
[EnableCors("AllowedPolicies")]
public class BaseApiController : ControllerBase
{
}
