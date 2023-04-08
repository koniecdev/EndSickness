namespace EndSickness.Shared.Medicines.Queries.GetMedicineById;

public record GetMedicineByIdQuery : IRequest<GetMedicineByIdDto>
{
    public GetMedicineByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; init; }
}
