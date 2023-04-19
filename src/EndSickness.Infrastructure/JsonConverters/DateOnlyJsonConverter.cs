using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EndSickness.Infrastructure.JsonConverters;
public sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
#pragma warning disable IDE1006 // Naming Styles
    private const string Format = "dd-MM-yyyy";
#pragma warning restore IDE1006 // Naming Styles
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.ParseExact(reader.GetString()!, Format, CultureInfo.GetCultureInfoByIetfLanguageTag("pl-PL"));
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format, CultureInfo.GetCultureInfoByIetfLanguageTag("pl-PL")));
    }
}