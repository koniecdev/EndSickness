using EndSickness.Shared.Medicines.Queries.GetDosages;

namespace EndSickness.Application.Medicines.Queries.GetDosages;

public class GetDosagesQueryHandler : IRequestHandler<GetDosagesQuery, GetDosagesVm>
{
    private readonly IEndSicknessContext _db;
    private readonly ICurrentUserService _currentUser;

    public GetDosagesQueryHandler(IEndSicknessContext db, ICurrentUserService currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }

    public async Task<GetDosagesVm> Handle(GetDosagesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
