using ToDoFlow.Application.Services.Interface;
using ToDoFlow.Domain.Models.Enums;

namespace ToDoFlow.Application.Services.Utils
{
    public class EnumService : IEnumService
    {
        public ApiResponse<Dictionary<int, string>> GetPriorities()
        {
            try
            {
                var priorities = Enum.GetValues(typeof(Priority)).Cast<Priority>().ToDictionary(p => (int)p, p => p.ToString());
                return new ApiResponse<Dictionary<int, string>>(priorities, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<Dictionary<int, string>>(null, false, $"{ex.Message}", 500);
            }
        }
    }
}
