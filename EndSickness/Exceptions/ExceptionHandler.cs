using EndSickness.Exceptions.Interfaces;

namespace EndSickness.ExceptionsHandling;

public class ExceptionHandler
{
    private readonly IExceptionHandlingStrategy _exceptionHandlerStrategy;
    private readonly Exception _exception;

    public ExceptionHandler(IExceptionHandlingStrategy exceptionHandlerStrategy, Exception exception)
    {
        _exceptionHandlerStrategy = exceptionHandlerStrategy;
        _exception = exception;
    }

    public IExceptionResponse Handle()
    {
        return _exceptionHandlerStrategy.Handle(_exception);
    }
}
