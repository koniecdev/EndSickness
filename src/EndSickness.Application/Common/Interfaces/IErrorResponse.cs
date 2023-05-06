namespace EndSickness.Application.Common.Interfaces;

public interface IErrorResponse
{
    string Message { get; set; }
    int ErrorCode { get; set; }
}
