namespace EndSickness.Exceptions.Interfaces;

public interface IExceptionHandlingStrategy
{
    IExceptionResponse Handle(Exception exception);
}
