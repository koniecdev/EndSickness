using AutoMapper;
using EndSickness.Application.Common.Interfaces;
using EndSickness.Shared.Medicines.Queries.GetUpdateMedicine;
using Microsoft.EntityFrameworkCore;

namespace EndSickness.Application.Medicines.Queries.GetUpdateMedicine;

public class GetUpdateMedicineQueryHandler : IRequestHandler<GetUpdateMedicineQuery, GetUpdateMedicineVm>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;

    public GetUpdateMedicineQueryHandler(IEndSicknessContext db, IMapper mapper, ICurrentUserService currentUser)
    {
        _db = db;
        _mapper = mapper;
        _currentUser = currentUser;
    }
    public async Task<GetUpdateMedicineVm> Handle(GetUpdateMedicineQuery request, CancellationToken cancellationToken)
    {
        var fromDb = await _db.Medicines.Include(m=>m.AppUser).SingleAsync(m => m.Id == request.Id, cancellationToken);
        if (_currentUser.AppUserId != fromDb.AppUser.UserId)
        {
            throw new UnauthorizedAccessException();
        }
        var vm = new GetUpdateMedicineVm()
        {
            Medicine = _mapper.Map<GetUpdateMedicineMedicineDto>(fromDb)
        };
        ArgumentNullException.ThrowIfNull(vm.Medicine);
        return vm;
    }
}
