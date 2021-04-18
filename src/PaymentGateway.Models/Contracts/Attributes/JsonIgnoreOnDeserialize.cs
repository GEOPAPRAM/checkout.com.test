using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PaymentGateway.Models.Contracts.Attributes
{
    /// <summary>
    /// Prevents deserialization of ignored property from JSON. Propert will still be serializat into JSON. 
    /// </summary> 
    public class JsonIgnoreOnDeserialize : JsonConverterAttribute
    {
        public JsonIgnoreOnDeserialize() : base(typeof(JsonIgnoreOnDeserializeConverter)) { }
    }

    /// <summary>
    /// Prevents deserialization of ignored property during conversation from JSON.
    /// </summary>
    public class JsonIgnoreOnDeserializeConverter : JsonConverter<object>
    {
        /// <inheritdoc />
        public override bool CanConvert(Type typeToConvert) => true;

        /// <summary>
        /// Prevents the conversion of reference types, but supports deserialization to default value types from JSON.
        /// </summary>
        /// <param name="reader">The reader</param>
        /// <param name="typeToConvert">The type to convert</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        /// <returns>The converted value.</returns>
        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return typeToConvert.IsValueType ? Activator.CreateInstance(typeToConvert) : null;

        }

        /// <inheritdoc /> 
        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}