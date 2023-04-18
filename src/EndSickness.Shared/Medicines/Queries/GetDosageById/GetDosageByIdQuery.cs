namespace EndSickness.Shared.Medicines.Queries.GetDosageById;

public record GetDosageByIdQuery : IRequest<GetDosageByIdVm>
{
    public GetDosageByIdQuery(int medicineId)
    {
        MedicineId = medicineId;
    }
    public int MedicineId { get; init; }
}
