using ToDoFlow.Domain.Models.Enums;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.Services.Services.Utils
{
    public class EnumService : IEnumService
    {
        public ApiResponse<Dictionary<int, string>> ReadPriorities()
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
