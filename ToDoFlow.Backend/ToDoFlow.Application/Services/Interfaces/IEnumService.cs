using ToDoFlow.Application.Services.Utils;

namespace ToDoFlow.Application.Services.Interfaces
{
    public interface IEnumService
    {
        public ApiResponse<Dictionary<int, string>> GetPriorities();
    }
}
