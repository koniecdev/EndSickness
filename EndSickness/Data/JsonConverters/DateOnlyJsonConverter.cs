using Newtonsoft.Json;
using System.Globalization;
namespace EndSickness.Data.JsonConverters;

public class DateOnlyConverter : JsonConverter<DateOnly>
{
    public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }

    public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String && reader.Value is string strValue)
        {
            if (DateOnly.TryParse(strValue, out var date))
            {
                return date;
            }
            else
            {
                throw new JsonException($"Failed to parse '{strValue}' as a valid DateOnly value.");
            }
        }
        else
        {
            throw new JsonException($"Expected a string value for DateOnly, but got {reader.TokenType}.");
        }
    }
}