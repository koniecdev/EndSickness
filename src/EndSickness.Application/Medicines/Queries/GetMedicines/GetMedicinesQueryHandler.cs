using AutoMapper;
using EndSickness.Application.Common.Exceptions;
using EndSickness.Application.Common.Interfaces;
using EndSickness.Shared.Medicines.Queries.GetMedicines;

namespace EndSickness.Application.Medicines.Queries.GetMedicines;

public class GetMedicinesQueryHandler : IRequestHandler<GetMedicinesQuery, GetMedicinesVm>
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

    public async Task<GetMedicinesVm> Handle(GetMedicinesQuery request, CancellationToken cancellationToken)
    {
        var result = await _db.Medicines.Where(m => m.StatusId != 0 && m.OwnerId == _currentUser.AppUserId)
            .ProjectTo<GetMedicinesDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        if(result.Count == 0)
        {
            throw new EmptyResultException();
        }
        return new GetMedicinesVm(result);
    }
}
