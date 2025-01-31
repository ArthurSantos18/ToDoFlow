using System.Text.Json.Serialization;

namespace ToDoFlow.Domain.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Profile
    {
        Administrator = 0,
        Default = 1
    }
}
