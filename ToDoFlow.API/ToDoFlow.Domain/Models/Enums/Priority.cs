using System.Text.Json.Serialization;

namespace ToDoFlow.Domain.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Priority
    {
        Baixa = 0,
        Média = 1,
        Alta = 2,
        Crítico = 3
    }
}
