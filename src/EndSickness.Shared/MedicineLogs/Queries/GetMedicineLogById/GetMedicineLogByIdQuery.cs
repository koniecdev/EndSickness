using EndSickness.Shared.Dtos;

namespace EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogById;

public record GetMedicineLogByIdQuery : IRequest<MedicineLogDto>
{
    public GetMedicineLogByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; init; }
}
