using EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;

namespace EndSickness.Application.MedicineLogs.Commands.DeleteMedicineLogsByMedicineId;

public class DeleteMedicineLogsByMedicineIdCommandHandler : IRequestHandler<DeleteMedicineLogsByMedicineIdCommand>
{
    private readonly IEndSicknessContext _db;

    public DeleteMedicineLogsByMedicineIdCommandHandler(IEndSicknessContext db)
    {
        _db = db;
    }
    public async Task Handle(DeleteMedicineLogsByMedicineIdCommand request, CancellationToken cancellationToken)
    {
        _db.MedicineLogs.RemoveRange(await _db.MedicineLogs.Where(m => m.MedicineId == request.MedicineId && m.StatusId != 0).ToArrayAsync(cancellationToken));
        await _db.SaveChangesAsync(cancellationToken);
    }
}
