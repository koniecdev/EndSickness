using EndSickness.Shared.Medicines.Commands.CreateMedicine;
using EndSickness.Shared.Medicines.Commands.DeleteMedicine;
using EndSickness.Shared.Medicines.Commands.UpdateMedicine;
using EndSickness.Shared.Medicines.Queries.GetMedicineById;
using EndSickness.Shared.Medicines.Queries.GetMedicines;

namespace EndSickness.API.Controllers.UserInterface;

[Authorize]
[Route("api/v1/medicines")]
public class MedicinesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<GetMedicinesVm>> Get()
    {
        var result = await Mediator.Send(new GetMedicinesQuery());
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetMedicineByIdVm>> Get(int id)
    {
        var result = await Mediator.Send(new GetMedicineByIdQuery(id));
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateMedicineCommand command)
    {
        var result = await Mediator.Send(command);
        return result;
    }

    [HttpPatch]
    public async Task Update(UpdateMedicineCommand command)
    {
        await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await Mediator.Send(new DeleteMedicineCommand(id));
    }
}
