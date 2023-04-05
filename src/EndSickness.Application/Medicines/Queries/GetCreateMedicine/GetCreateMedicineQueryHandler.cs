using EndSickness.Application.Common.Interfaces;
using EndSickness.Shared.Medicines.Queries.GetCreateMedicine;

namespace EndSickness.Application.Medicines.Queries.GetCreateMedicine;

public class GetCreateMedicineQueryHandler : IRequestHandler<GetCreateMedicineQuery, GetCreateMedicineVm>
{
    public GetCreateMedicineQueryHandler()
    {
    }
    public async Task<GetCreateMedicineVm> Handle(GetCreateMedicineQuery request, CancellationToken cancellationToken)
    {
        GetCreateMedicineVm result = await Task.Run(() =>  new GetCreateMedicineVm(), cancellationToken);
        return result;
    }
}
