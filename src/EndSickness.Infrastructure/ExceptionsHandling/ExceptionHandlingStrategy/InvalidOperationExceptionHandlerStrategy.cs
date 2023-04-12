using System.Net;

namespace EndSickness.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;

public class InvalidOperationExceptionHandlerStrategy : IExceptionHandlerStrategy
{
    public (int statusCode, string errorMessage) Handle(Exception exception)
    {
        string errorMessage = ResolveStatusCode(exception.Message, out int statusCode);
        return (statusCode, errorMessage);
    }

    private static string ResolveStatusCode(string exceptionMessage, out int statusCode)
    {
        if (exceptionMessage.Contains("Sequence contains no element"))
        {
            statusCode = (int)HttpStatusCode.NotFound;
            return "Could not find resource";
        }
        else
        {
            statusCode = (int)HttpStatusCode.InternalServerError;
            return "We're sorry, some error occured, please get in touch with site administrator.";
        }
    }
}
