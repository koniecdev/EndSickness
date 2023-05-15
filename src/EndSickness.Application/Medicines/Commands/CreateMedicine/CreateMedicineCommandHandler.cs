using EndSickness.Domain.Entities;
using EndSickness.Shared.Medicines.Commands.CreateMedicine;

namespace EndSickness.Application.Medicines.Commands.CreateMedicine;

public class CreateMedicineCommandHandler : IRequestHandler<CreateMedicineCommand, int>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;

    public CreateMedicineCommandHandler(IEndSicknessContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateMedicineCommand request, CancellationToken cancellationToken)
    {
        var medicineToInsert = _mapper.Map<Medicine>(request);
        _db.Medicines.Add(medicineToInsert);
        await _db.SaveChangesAsync(cancellationToken);
        return medicineToInsert.Id;
    }
}
