namespace EndSickness.Application.Common.Exceptions;

public class EmptyResultException : Exception
{
    public EmptyResultException() : base($"Request yielded no results")
    {
        
    }
}