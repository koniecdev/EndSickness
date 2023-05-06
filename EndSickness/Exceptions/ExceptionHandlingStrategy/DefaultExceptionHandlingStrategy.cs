using System.Net;
using EndSickness.Exceptions.Interfaces;

namespace EndSickness.ExceptionsHandling.ExceptionHandlingStrategy;

public class DefaultExceptionHandlingStrategy : IExceptionHandlingStrategy
{
    private readonly IExceptionResponse _response;

    public DefaultExceptionHandlingStrategy(IExceptionResponse response)
    {
        _response = response;
    }

    public IExceptionResponse Handle(Exception exception)
    {
        _response.StatusCode = (int)HttpStatusCode.InternalServerError;
        _response.Message = exception.Message;
        return _response;
    }
}
