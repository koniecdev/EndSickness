using EndSickness.Shared.Medicines.Commands.DeleteMedicine;

namespace EndSickness.Application.Medicines.Commands.DeleteMedicine;

public class DeleteMedicineCommandHandler : IRequestHandler<DeleteMedicineCommand>
{
    private readonly IEndSicknessContext _db;

    public DeleteMedicineCommandHandler(IEndSicknessContext db)
    {
        _db = db;
    }
    public async Task Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
    {
        var fromDb = await _db.Medicines.SingleAsync(m => m.StatusId != 0 && m.Id == request.Id, cancellationToken);
        _db.Medicines.Remove(fromDb);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
