﻿using EndSickness.Shared.Dtos;

namespace EndSickness.Shared.Medicines.Queries.GetMedicineById;

public record GetMedicineByIdQuery : IRequest<MedicineDto>
{
    public GetMedicineByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; init; }
}
