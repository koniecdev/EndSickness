namespace EndSickness.Application.Common.Interfaces.ExceptionsHandling;

public interface IExceptionHandlerStrategy
{
    (int statusCode, string errorMessage) Handle(Exception exception);
}
