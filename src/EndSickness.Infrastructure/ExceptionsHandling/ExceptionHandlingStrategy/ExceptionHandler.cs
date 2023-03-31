namespace EndSickness.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;

public class ExceptionHandler
{
    private readonly IExceptionHandlerStrategy _exceptionHandlerStrategy;
    private readonly Exception _exception;

    public ExceptionHandler(IExceptionHandlerStrategy exceptionHandlerStrategy, Exception exception)
    {
        _exceptionHandlerStrategy = exceptionHandlerStrategy;
        _exception = exception;
    }

    public (int statusCode, string errorMessage) Handle()
    {
        return _exceptionHandlerStrategy.Handle(_exception);
    }
}
