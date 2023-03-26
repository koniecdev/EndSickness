namespace EndSickness.Domain.Exceptions;

internal class UninitializedNavigationPropertyAccessException : Exception
{
    internal UninitializedNavigationPropertyAccessException(string propertyName) : base($"Trying to access Uninitialized Navigation property: {propertyName}")
    {
        
    }
}
