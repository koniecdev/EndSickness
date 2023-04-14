using FluentValidation;
using System.Net;

namespace EndSickness.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;

public class ValidationExceptionHandlerStrategy : IExceptionHandlerStrategy
{
    public (int statusCode, string errorMessage) Handle(Exception exception)
    {
        int statusCode = (int)HttpStatusCode.BadRequest;
        ValidationException validationException = (ValidationException)exception;
        string errorMessage = validationException.ToString();
        return (statusCode, errorMessage);
    }
}
