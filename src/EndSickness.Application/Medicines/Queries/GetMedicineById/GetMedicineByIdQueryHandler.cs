using AutoMapper;
using AutoMapper.QueryableExtensions;
using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.Common.Interfaces;
using EndSickness.Shared.Medicines.Queries.GetMedicineById;

namespace EndSickness.Application.Medicines.Queries.GetMedicineById;

public class GetMedicineByIdQueryHandler : IRequestHandler<GetMedicineByIdQuery, GetMedicineByIdVm>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;

    public GetMedicineByIdQueryHandler(IEndSicknessContext db, IMapper mapper, ICurrentUserService currentUser)
    {
        _db = db;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<GetMedicineByIdVm> Handle(GetMedicineByIdQuery request, CancellationToken cancellationToken)
    {
        var fromDb = await _db.Medicines.Where(m => m.StatusId != 0 && m.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
        if (fromDb is null)
        {
            throw new ResourceNotFoundException();
        }
        else
        {
            _currentUser.CheckOwnership(fromDb.OwnerId);
            return _mapper.Map<GetMedicineByIdVm>(fromDb);
        }
    }
}
