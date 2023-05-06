using EndSickness.Exceptions.Interfaces;

namespace EndSickness.Exceptions.ApplicationExceptions;

public class ApiUnsuccessfullResultException : Exception
{
    public ApiUnsuccessfullResultException(IExceptionResponse errorResponse) : base(errorResponse.Message)
    {
    }
}
