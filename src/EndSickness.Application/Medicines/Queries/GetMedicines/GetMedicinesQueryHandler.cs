using AutoMapper;
using EndSickness.Application.Common.Interfaces;
using EndSickness.Shared.Medicines.Queries.GetMedicines;

namespace EndSickness.Application.Medicines.Queries.GetMedicines;

public class GetMedicinesQueryHandler : IRequestHandler<GetMedicinesQuery, GetMedicinesDto>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;

    public GetMedicinesQueryHandler(IEndSicknessContext db, IMapper mapper, ICurrentUserService currentUser)
    {
        _db = db;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task<GetMedicinesDto> Handle(GetMedicinesQuery request, CancellationToken cancellationToken)
    {
        var fromDb = await _db.Medicines.Include(m=>m.AppUser)
            .Where(m => m.StatusId != 0 && m.AppUser.UserId == _currentUser.AppUserId)
            .ToListAsync(cancellationToken);
        var mapped = _mapper.Map<ICollection<GetMedicinesMedicineDto>>(fromDb);
        return mapped.Count > 0 ? new GetMedicinesDto(mapped) : new GetMedicinesDto();
    }
}
