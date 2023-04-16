namespace EndSickness.Shared.MedicineLogs.Queries.GetMedicineLogById;

public record GetMedicineLogByIdQuery : IRequest<GetMedicineLogByIdVm>
{
    public GetMedicineLogByIdQuery(int id)
    {
        Id = id;
    }
    public int Id { get; init; }
}
