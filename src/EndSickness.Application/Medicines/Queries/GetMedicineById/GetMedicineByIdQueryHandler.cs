using AutoMapper;
using EndSickness.Application.Common.Interfaces;
using EndSickness.Shared.Medicines.Queries.GetMedicineById;

namespace EndSickness.Application.Medicines.GetMedicineById.GetCreateMedicine;

public class GetMedicineByIdQueryHandler : IRequestHandler<GetMedicineByIdQuery, GetMedicineByIdDto>
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

    public async Task<GetMedicineByIdDto> Handle(GetMedicineByIdQuery request, CancellationToken cancellationToken)
    {
        //var fromDb = await _db.Medicines
        //    .SingleAsync(m=>m.Id == request.Id, cancellationToken);
        //_currentUser.IsAuthorized(fromDb.AppUserId);
        throw new NotImplementedException();
    }
}
