using System.Text.Json;
using System.Text.Json.Serialization;
using Wallet.Domain.ValueObjects;

namespace Wallet.Infra.Serialization.Converters;

public class PasswordJsonConverter : JsonConverter<Password>
{
    public override Password? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var password = reader.GetString();
        return new Password(password ?? "");
    }

    public override void Write(Utf8JsonWriter writer, Password value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}