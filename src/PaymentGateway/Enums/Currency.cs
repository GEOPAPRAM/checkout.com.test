using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PaymentGateway.Enums
{
    // [JsonConverter(typeof(StringEnumConverter))]
    public enum Currency
    {
        GBP = 1,
        EUR = 2,
        USD = 3,
        JPY = 4,
        AUD = 5
    }
}