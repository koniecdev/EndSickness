using AutoMapper;
using EndSickness.Application.Common.Interfaces;
using EndSickness.Domain.Entities;
using EndSickness.Shared.Medicines.Commands.CreateMedicine;

namespace EndSickness.Application.Medicines.Commands.CreateMedicine;

public class CreateMedicineCommandHandler : IRequestHandler<CreateMedicineCommand, int>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;

    public CreateMedicineCommandHandler(IEndSicknessContext db, IMapper mapper, ICurrentUserService currentUser)
    {
        _db = db;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<int> Handle(CreateMedicineCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Medicine>(request);
        _db.Medicines.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
