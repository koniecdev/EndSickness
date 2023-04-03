using System.Net;

namespace EndSickness.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;

public class ArgumentNullExceptionHandlerStrategy : IExceptionHandlerStrategy
{
    public (int statusCode, string errorMessage) Handle(Exception exception)
    {
        int statusCode = (int)HttpStatusCode.BadRequest;
        string errorMessage = $"Internal server error! \n {exception.Message}";
        return (statusCode, errorMessage);
    }
}
