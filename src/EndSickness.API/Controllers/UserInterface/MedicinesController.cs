using EndSickness.Shared.Medicines.Queries.GetCreateMedicine;
using EndSickness.Shared.Medicines.Queries.GetUpdateMedicine;

namespace EndSickness.API.Controllers.UserInterface;

[Authorize]
[Route("api/v1/medicines")]
public class MedicinesController : BaseApiController
{
    [HttpGet("create")]
    public async Task<ActionResult<GetCreateMedicineVm>> GetCreate()
    {
        var result = await Mediator.Send(new GetCreateMedicineQuery());
        return result;
    }

    [HttpGet("{id}/update")]
    public async Task<ActionResult<GetUpdateMedicineVm>> GetUpdate(int id)
    {
        var result = await Mediator.Send(new GetUpdateMedicineQuery(id));
        return result;
    }
}
