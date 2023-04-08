using System.Net;

namespace EndSickness.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;

public class UnauthorizedAccessExceptionHandlerStrategy : IExceptionHandlerStrategy
{
    public (int statusCode, string errorMessage) Handle(Exception exception)
    {
        int statusCode = (int)HttpStatusCode.Unauthorized;
        string errorMessage = $"{exception.Message}";
        return (statusCode, errorMessage);
    }
}
