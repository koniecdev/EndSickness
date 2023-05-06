using EndSickness.Exceptions.Interfaces;

namespace EndSickness.Exceptions;

public class ExceptionResponse : IExceptionResponse
{
    public string Message { get; set; } = "";
    public int StatusCode { get; set; }
}
