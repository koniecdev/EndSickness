namespace EndSickness.Application.Common.Exceptions;

public class OverdoseException : Exception
{
    public OverdoseException() : base($"We do not support scenario when You overdose medicine. For any questions, get in touch with professional medical help.")
    {

    }
}