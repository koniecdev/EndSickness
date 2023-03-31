namespace EndSickness.Infrastructure.ExceptionsHandling.ExceptionHandlingStrategy;

public interface IExceptionHandlerStrategy
{
    (int statusCode, string errorMessage) Handle(Exception exception);
}
