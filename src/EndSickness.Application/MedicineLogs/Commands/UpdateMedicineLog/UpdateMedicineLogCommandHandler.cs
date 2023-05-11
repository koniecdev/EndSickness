using EndSickness.Shared.MedicineLogs.Commands.UpdateMedicineLog;

namespace EndSickness.Application.MedicineLogs.Commands.UpdateMedicineLog;

public class UpdateMedicineLogCommandHandler : IRequestHandler<UpdateMedicineLogCommand>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;

    public UpdateMedicineLogCommandHandler(IEndSicknessContext db, IMapper mapper, IResourceOwnershipService ownershipService)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task Handle(UpdateMedicineLogCommand request, CancellationToken cancellationToken)
    {
        var fromDb = await _db.MedicineLogs.SingleAsync(m => m.StatusId != 0 && m.Id == request.Id, cancellationToken);
        _mapper.Map(request, fromDb);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
