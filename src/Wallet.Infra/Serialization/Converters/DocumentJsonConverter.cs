using System.Text.Json;
using System.Text.Json.Serialization;
using Wallet.Domain.ValueObjects;

namespace Wallet.Infra.Serialization.Converters;

public class DocumentJsonConverter : JsonConverter<DocumentNumber>
{
    public override DocumentNumber? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var number = reader.GetString();
        return new DocumentNumber(number ?? "");
    }

    public override void Write(Utf8JsonWriter writer, DocumentNumber value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}