using AutoMapper;
using EndSickness.Application.Common.Interfaces;
using EndSickness.Shared.Medicines.Commands.DeleteMedicine;

namespace EndSickness.Application.Medicines.Commands.DeleteMedicine;

public class DeleteMedicineCommandHandler : IRequestHandler<DeleteMedicineCommand>
{
    private readonly IEndSicknessContext _db;
    private readonly ICurrentUserService _currentUser;

    public DeleteMedicineCommandHandler(IEndSicknessContext db, ICurrentUserService currentUser)
    {
        _db = db;
        _currentUser = currentUser;
    }
    public async Task Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
