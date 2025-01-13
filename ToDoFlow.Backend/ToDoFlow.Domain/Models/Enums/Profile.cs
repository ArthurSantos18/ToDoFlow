using System.Text.Json.Serialization;

namespace ToDoFlow.Domain.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Profile
    {
        Adm = 0,
        Padrão = 1
    }
}
