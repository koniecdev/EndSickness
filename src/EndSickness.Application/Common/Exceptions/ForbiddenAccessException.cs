namespace EndSickness.Application.Common.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException() : base($"You have no access to requested resource")
    {
        
    }
}