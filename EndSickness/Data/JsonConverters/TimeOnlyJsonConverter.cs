using Newtonsoft.Json;
using System.Globalization;
//using System.Text.Json;
//using System.Text.Json.Serialization;

namespace EndSickness.Data.JsonConverters;
//public class TimeOnlyConverter : JsonConverter<TimeOnly>
//{
//#pragma warning disable IDE1006 // Naming Styles
//    private const string Format = "HH:mm:ss";
//#pragma warning restore IDE1006 // Naming Styles
//    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
//    {
//        return TimeOnly.ParseExact(reader.GetString()!, Format, CultureInfo.GetCultureInfoByIetfLanguageTag("pl-PL"));
//    }

//    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
//    {
//        writer.WriteStringValue(value.ToString(Format, CultureInfo.GetCultureInfoByIetfLanguageTag("pl-PL")));
//    }
//}

public class TimeOnlyConverter : JsonConverter<TimeOnly>
{
    public override void WriteJson(JsonWriter writer, TimeOnly value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }

    public override TimeOnly ReadJson(JsonReader reader, Type objectType, TimeOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String && reader.Value is string strValue)
        {
            if (TimeOnly.TryParse(strValue, out var time))
            {
                return time;
            }
            else
            {
                throw new JsonException($"Failed to parse '{strValue}' as a valid TimeOnly value.");
            }
        }
        else
        {
            throw new JsonException($"Expected a string value for TimeOnly, but got {reader.TokenType}.");
        }
    }
}