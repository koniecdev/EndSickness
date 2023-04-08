using EndSickness.Shared.Medicines.Commands.DeleteMedicine;

namespace EndSickness.Application.Medicines.Commands.DeleteMedicine;

public class DeleteMedicineCommandHandler : IRequestHandler<DeleteMedicineCommand>
{
    public async Task Handle(DeleteMedicineCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
