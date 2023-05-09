using EndSickness.Exceptions.Interfaces;
using EndSickness.ExceptionsHandling;
using EndSickness.ExceptionsHandling.ExceptionHandlingStrategy;
using Newtonsoft.Json;
using System.Reflection;

namespace EndSickness.Extensions;

public static class ReflectionExtension
{
    public static string GetPropertyValue<T>(this T item, string propertyName)
    {
        return item?.GetType()?.GetProperty(propertyName)?.GetValue(item, null)?.ToString() ?? "";
    }
}