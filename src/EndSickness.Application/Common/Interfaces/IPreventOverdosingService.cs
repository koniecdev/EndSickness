namespace EndSickness.Application.Common.Interfaces
{
    public interface IPreventOverdosingService
    {
        Task HandleAsync(int medicineId, DateTime newDoseDateTime, CancellationToken cancellationToken);
    }
}