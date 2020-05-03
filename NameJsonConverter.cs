using System;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace raven_poc
{
    internal class NameJsonConverter : JsonConverter<Name>
    {
        public override Name ReadJson(JsonReader reader, Type objectType, [AllowNull] Name existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = reader.Value.ToString();
            return string.IsNullOrWhiteSpace(value) ? null : new Name(value);
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] Name value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}