using EndSickness.Shared.Medicines.Queries.GetMedicineById;
using EndSickness.Shared.Medicines.Queries.GetMedicines;

namespace EndSickness.API.Controllers.UserInterface;

[Authorize]
[Route("api/v1/medicines")]
public class MedicinesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<GetMedicinesDto>> Get()
    {
        var result = await Mediator.Send(new GetMedicinesQuery());
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetMedicineByIdDto>> Get(int id)
    {
        var result = await Mediator.Send(new GetMedicineByIdQuery(id));
        return result;
    }
}
