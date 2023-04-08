using EndSickness.Shared.Medicines.Queries.GetMedicines;

namespace EndSickness.Application.Medicines.GetMedicineById.GetMedicines;

public class GetMedicinesQueryHandler : IRequestHandler<GetMedicinesQuery, GetMedicinesDto>
{
    public GetMedicinesQueryHandler()
    {
    }

    public async Task<GetMedicinesDto> Handle(GetMedicinesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
