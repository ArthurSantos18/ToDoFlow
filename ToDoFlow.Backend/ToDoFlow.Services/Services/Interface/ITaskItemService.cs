using ToDoFlow.Application.Dtos;
using ToDoFlow.Services.Services.Utils;

namespace ToDoFlow.Services.Services.Interface
{
    public interface ITaskItemService
    {
        public Task<ApiResponse<TaskItemReadDto>> CreateTaskItemAsync(int userId, TaskItemCreateDto taskItemCreateDto);
        public Task<ApiResponse<IEnumerable<TaskItemReadDto>>> GetTaskItemAsync();
        public Task<ApiResponse<IEnumerable<TaskItemReadDto>>> GetTaskItemByUserAsync(int userId);
        public Task<ApiResponse<IEnumerable<TaskItemReadDto>>> GetTaskItemByCategoryAsync(int categoryId, int userId);
        public Task<ApiResponse<TaskItemReadDto>> GetTaskItemByIdAsync(int id, int userId);
        public Task<ApiResponse<TaskItemReadDto>> UpdateTaskItemAsync(int id, int userId, TaskItemUpdateDto taskItemUpdateDto);
        public Task<ApiResponse> DeleteTaskItemAsync(int id, int userId);
    }
}
