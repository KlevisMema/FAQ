using System.Text.Json.Serialization;

namespace FAQ.SHARED.Enums
{
    /// <summary>
    ///     Gender Enum starting from 1.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        Male = 1,
        Female = 2,
        UnSpecified = 3
    }
}