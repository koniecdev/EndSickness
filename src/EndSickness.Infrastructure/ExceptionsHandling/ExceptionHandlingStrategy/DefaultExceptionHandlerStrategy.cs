using System.Net;

namespace EndSickness.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;

public class DefaultExceptionHandlerStrategy : IExceptionHandlerStrategy
{
    public (int statusCode, string errorMessage) Handle(Exception exception)
    {
        int statusCode = (int)HttpStatusCode.InternalServerError;
        string errorMessage = $"Internal server error! \n {exception.Message}";
        return (statusCode, errorMessage);
    }
}
