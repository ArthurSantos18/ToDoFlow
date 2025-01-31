using System.Text.Json.Serialization;

namespace ToDoFlow.Domain.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        Pending = 0,
        InProgress = 1,
        Complete = 2,
    }
}
