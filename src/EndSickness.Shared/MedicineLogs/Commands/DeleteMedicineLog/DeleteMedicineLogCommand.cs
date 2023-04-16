namespace EndSickness.Shared.MedicineLogs.Commands.DeleteMedicineLog;

public record DeleteMedicineLogCommand : IRequest
{
    public DeleteMedicineLogCommand()
    {

    }
    public DeleteMedicineLogCommand(int id)
    {
        Id = id;
    }
    public int Id { get; init; }
}
