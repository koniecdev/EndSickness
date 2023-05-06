using EndSickness.Shared.MedicineLogs.Commands.CreateMedicineLog;
using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLog;
using EndSickness.Shared.MedicineLogs.Commands.UpdateMedicineLog;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogById;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogs;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogsByMedicineId;

namespace EndSickness.API.Controllers;

[Authorize]
[Route("api/v1/medicine-logs")]
public class MedicineLogsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<GetMedicineLogsVm>> Get()
    {
        var result = await Mediator.Send(new GetMedicineLogsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetMedicineLogByIdVm>> Get(int id)
    {
        var result = await Mediator.Send(new GetMedicineLogByIdQuery(id));
        return Ok(result);
    }

    [HttpGet("medicine/{id}")]
    public async Task<ActionResult<GetMedicineLogsByMedicineIdVm>> GetByMedicineId(int id)
    {
        var result = await Mediator.Send(new GetMedicineLogsByMedicineIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateMedicineLogCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpPatch]
    public async Task<ActionResult> Update(UpdateMedicineLogCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await Mediator.Send(new DeleteMedicineLogCommand(id));
        return NoContent();
    }
}
