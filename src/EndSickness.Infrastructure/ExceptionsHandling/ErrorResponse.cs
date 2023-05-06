namespace EndSickness.Infrastructure.ExceptionsHandling;

public class ErrorResponse : IErrorResponse
{
    public ErrorResponse()
    {
        Message = string.Empty;
    }
    public string Message { get; set; }
    public int ErrorCode { get; set; }
}
