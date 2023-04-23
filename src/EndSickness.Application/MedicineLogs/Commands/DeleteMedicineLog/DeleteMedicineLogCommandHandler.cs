using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLog;

namespace EndSickness.Application.MedicineLogs.Commands.DeleteMedicineLog;

public class DeleteMedicineLogCommandHandler : IRequestHandler<DeleteMedicineLogCommand>
{
    private readonly IEndSicknessContext _db;

    public DeleteMedicineLogCommandHandler(IEndSicknessContext db)
    {
        _db = db;
    }
    public async Task Handle(DeleteMedicineLogCommand request, CancellationToken cancellationToken)
    {
        var fromDb = await _db.MedicineLogs.SingleAsync(m => m.StatusId != 0 && m.Id == request.Id, cancellationToken);
        _db.MedicineLogs.Remove(fromDb);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
