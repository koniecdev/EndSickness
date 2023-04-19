using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EndSickness.Infrastructure.JsonConverters;
public sealed class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
#pragma warning disable IDE1006 // Naming Styles
    private const string Format = "HH:mm:ss";
#pragma warning restore IDE1006 // Naming Styles
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return TimeOnly.ParseExact(reader.GetString()!, Format, CultureInfo.GetCultureInfoByIetfLanguageTag("pl-PL"));
    }

    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format, CultureInfo.GetCultureInfoByIetfLanguageTag("pl-PL")));
    }
}