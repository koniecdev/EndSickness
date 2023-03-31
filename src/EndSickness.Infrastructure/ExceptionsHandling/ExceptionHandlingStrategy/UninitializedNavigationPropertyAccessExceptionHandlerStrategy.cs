using System.Net;

namespace EndSickness.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;

public class UninitializedNavigationPropertyAccessExceptionHandlerStrategy : IExceptionHandlerStrategy
{
    public (int statusCode, string errorMessage) Handle(Exception exception)
    {
        int statusCode = (int)HttpStatusCode.BadRequest;
        string errorMessage = exception.Message;
        return (statusCode, errorMessage);
    }
}
