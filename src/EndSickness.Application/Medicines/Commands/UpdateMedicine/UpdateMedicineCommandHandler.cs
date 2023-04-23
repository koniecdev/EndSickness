using EndSickness.Shared.Medicines.Commands.UpdateMedicine;

namespace EndSickness.Application.Medicines.Commands.UpdateMedicine;

public class UpdateMedicineCommandHandler : IRequestHandler<UpdateMedicineCommand>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;

    public UpdateMedicineCommandHandler(IEndSicknessContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task Handle(UpdateMedicineCommand request, CancellationToken cancellationToken)
    {
        var fromDb = await _db.Medicines.SingleAsync(m => m.StatusId != 0 && m.Id == request.Id, cancellationToken);
        fromDb = _mapper.Map(request, fromDb);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
