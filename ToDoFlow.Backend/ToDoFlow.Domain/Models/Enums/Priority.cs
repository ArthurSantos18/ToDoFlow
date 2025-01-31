using System.Text.Json.Serialization;

namespace ToDoFlow.Domain.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Priority
    {
        Low = 0,
        Medium = 1,
        High = 2,
        Crítical = 3
    }
}
