using AutoMapper;
using EndSickness.Application.Common.Interfaces;
using EndSickness.Shared.Medicines.Commands.UpdateMedicine;

namespace EndSickness.Application.Medicines.Commands.UpdateMedicine;

public class UpdateMedicineCommandHandler : IRequestHandler<UpdateMedicineCommand>
{
    private readonly IEndSicknessContext _db;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUser;

    public UpdateMedicineCommandHandler(IEndSicknessContext db, IMapper mapper, ICurrentUserService currentUser)
    {
        _db = db;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    public async Task Handle(UpdateMedicineCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
