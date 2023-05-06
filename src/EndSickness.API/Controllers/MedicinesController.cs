using EndSickness.Shared.Medicines.Commands.CreateMedicine;
using EndSickness.Shared.Medicines.Commands.DeleteMedicine;
using EndSickness.Shared.Medicines.Commands.UpdateMedicine;
using EndSickness.Shared.Medicines.Queries.GetDosageById;
using EndSickness.Shared.Medicines.Queries.GetDosages;
using EndSickness.Shared.Medicines.Queries.GetMedicineById;
using EndSickness.Shared.Medicines.Queries.GetMedicines;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EndSickness.API.Controllers;

[Authorize]
[Route("api/v1/medicines")]
public class MedicinesController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<GetMedicinesVm>> Get()
    {
        var result = await Mediator.Send(new GetMedicinesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetMedicineByIdVm>> Get(int id)
    {
        var result = await Mediator.Send(new GetMedicineByIdQuery(id));
        return Ok(result);
    }

    [HttpGet("dosages")]
    public async Task<ActionResult<GetDosagesVm>> Dosages()
    {
        var result = await Mediator.Send(new GetDosagesQuery());
        return Ok(result);
    }

    [HttpGet("{id}/dosages")]
    public async Task<ActionResult<GetDosageByIdVm>> Dosages(int id)
    {
        var result = await Mediator.Send(new GetDosageByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateMedicineCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpPatch]
    public async Task<ActionResult> Update(UpdateMedicineCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteMedicineCommand(id));
        return NoContent();
    }
}
