namespace EndSickness.Application.Common.Exceptions;

public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException() : base($"Resource do not exists")
    {
        
    }
}