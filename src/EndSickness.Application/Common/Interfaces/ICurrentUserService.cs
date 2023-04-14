namespace EndSickness.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string AppUserId { get; }
    public bool IsAuthorized { get; }
}