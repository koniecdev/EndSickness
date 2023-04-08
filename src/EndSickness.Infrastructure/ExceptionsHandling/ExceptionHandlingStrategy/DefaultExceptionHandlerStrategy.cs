using System.Net;

namespace EndSickness.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;

public class DefaultExceptionHandlerStrategy : IExceptionHandlerStrategy
{
    public (int statusCode, string errorMessage) Handle(Exception exception)
    {
        int statusCode = (int)HttpStatusCode.InternalServerError;
        string errorMessage = "We're sorry, some error occured, please get in touch with site administrator.";
        return (statusCode, errorMessage);
    }
}
