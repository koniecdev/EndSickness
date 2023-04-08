namespace EndSickness.Application;

public interface ICurrentUserService
{
    string AppUserId { get; set; }
    public void IsAuthorized(string objectUserId);
}