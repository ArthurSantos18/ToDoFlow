using ToDoFlow.Application.Services.Utils;

namespace ToDoFlow.Application.Services.Interface
{
    public interface IEnumService
    {
        public ApiResponse<Dictionary<int, string>> GetPriorities();
    }
}
