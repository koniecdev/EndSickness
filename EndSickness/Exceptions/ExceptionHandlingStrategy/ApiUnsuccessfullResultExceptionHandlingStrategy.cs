using System.Net;
using EndSickness.Exceptions.Interfaces;

namespace EndSickness.ExceptionsHandling.ExceptionHandlingStrategy;

public class ApiUnsuccessfullResultExceptionHandlingStrategy : IExceptionHandlingStrategy
{
    private readonly IExceptionResponse _response;

    public ApiUnsuccessfullResultExceptionHandlingStrategy(IExceptionResponse response)
    {
        _response = response;
    }

    public IExceptionResponse Handle(Exception exception)
    {
        _response.StatusCode = (int)HttpStatusCode.BadRequest;
        _response.Message = exception.Message;
        return _response;
    }
}
