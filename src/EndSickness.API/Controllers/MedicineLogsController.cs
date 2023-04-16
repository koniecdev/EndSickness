using EndSickness.Shared.MedicineLogs.Commands.CreateMedicineLog;
using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLog;
using EndSickness.Shared.MedicineLogs.Commands.UpdateMedicineLog;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogById;
using EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogs;

namespace EndSickness.API.Controllers;

[Authorize]
[Route("api/v1/medicine-logs")]
public class MedicineLogsController : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<GetMedicineLogsVm>> Get()
    {
        var result = await Mediator.Send(new GetMedicineLogsQuery());
        return result;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetMedicineLogByIdVm>> Get(int id)
    {
        var result = await Mediator.Send(new GetMedicineLogByIdQuery(id));
        return result;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateMedicineLogCommand command)
    {
        var result = await Mediator.Send(command);
        return result;
    }

    [HttpPatch]
    public async Task Update(UpdateMedicineLogCommand command)
    {
        await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await Mediator.Send(new DeleteMedicineLogCommand(id));
    }
}
