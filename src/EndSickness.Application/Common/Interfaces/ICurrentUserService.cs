namespace EndSickness.Application;

public interface ICurrentUserService
{
    string AppUserId { get; }
    public bool IsAuthorized { get; }
    public void CheckOwnership(string ownerId);
}