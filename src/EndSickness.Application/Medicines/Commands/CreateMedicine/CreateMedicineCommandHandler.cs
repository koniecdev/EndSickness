using EndSickness.Shared.Medicines.Commands.CreateMedicine;

namespace EndSickness.Application.Medicines.Commands.CreateMedicine;

public class CreateMedicineCommandHandler : IRequestHandler<CreateMedicineCommand, int>
{
    public async Task<int> Handle(CreateMedicineCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
