namespace EndSickness.Application.Common.Exceptions;

public class UserNotResolvedException : Exception
{
    public UserNotResolvedException() : base($"Could not retrieve logged user who submitted request")
    {
        
    }
}