namespace EndSickness.Shared.Medicines.Commands.DeleteMedicine;

public record DeleteMedicineCommand : IRequest
{
    public DeleteMedicineCommand()
    {
        
    }
    public DeleteMedicineCommand(int id)
    {
        Id = id;
    }
    public int Id { get; init; }
}
