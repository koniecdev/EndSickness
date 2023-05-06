namespace EndSickness.Exceptions.Interfaces;

public interface IExceptionResponse
{
    int StatusCode { get; set; }
    string Message { get; set; }
}