namespace EndSickness.Shared.Medicines.Commands.DeleteMedicine;

public record DeleteMedicineCommand : IRequest
{
    public int Id { get; init; }
}
