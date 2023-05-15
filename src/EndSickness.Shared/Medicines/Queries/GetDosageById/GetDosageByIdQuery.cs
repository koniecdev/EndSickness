using EndSickness.Shared.Dtos;

namespace EndSickness.Shared.Medicines.Queries.GetDosageById;

public record GetDosageByIdQuery : IRequest<DosageDto>
{
    public GetDosageByIdQuery(int medicineId)
    {
        MedicineId = medicineId;
    }
    public int MedicineId { get; init; }
}
