using FluentValidation;
using System.Net;
using System.Text;

namespace EndSickness.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;

public class ValidationExceptionHandlerStrategy : IExceptionHandlerStrategy
{
    public (int statusCode, string errorMessage) Handle(Exception exception)
    {
        int statusCode = (int)HttpStatusCode.BadRequest;
        ValidationException validationException = (ValidationException)exception;
        StringBuilder errorMessageBuilder = new();
        foreach(var error in validationException.Errors)
        {
            errorMessageBuilder.Append(error.ErrorMessage);
            errorMessageBuilder.Append('\n');
        }
        return (statusCode, errorMessageBuilder.ToString());
    }
}
