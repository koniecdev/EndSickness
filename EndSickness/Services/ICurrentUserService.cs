namespace EndSickness.Services;
public interface ICurrentUserService
{
    string UserId { get; }
    string Email { get; }
    string Username { get; }

}