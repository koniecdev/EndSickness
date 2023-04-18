using EndSickness.Domain.Entities;
using EndSickness.Shared.MedicineLogs.Commands.CreateMedicineLog;

namespace EndSickness.Application.MedicineLogs.Commands.CreateMedicineLog;

public class CreateMedicineLogCommandHandler : IRequestHandler<CreateMedicineLogCommand, int>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;

    public CreateMedicineLogCommandHandler(IEndSicknessContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateMedicineLogCommand request, CancellationToken cancellationToken)
    {
        var mapped = _mapper.Map<MedicineLog>(request);
        var medFromDb = await _db.Medicines.SingleAsync(m => m.Id == request.MedicineId && m.StatusId != 0, cancellationToken);
        _db.MedicineLogs.Add(mapped);
        await _db.SaveChangesAsync(cancellationToken);
        return mapped.Id;
    }
}
