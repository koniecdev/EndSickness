namespace EndSickness.Domain.Common;

public abstract class AuditableEntity
{
    public int Id { get; set; }
    public int StatusId { get; set; } = 1;
    public string OwnerId { get; set; } = string.Empty;
}