namespace EndSickness.Domain.Exceptions;

public class UninitializedNavigationPropertyAccessException : Exception
{
    public UninitializedNavigationPropertyAccessException(string propertyName) : base($"Trying to access Uninitialized Navigation property: {propertyName}")
    {
        
    }
}
