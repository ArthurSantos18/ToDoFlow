using ToDoFlow.Application.Dtos;
using ToDoFlow.Services.Services.Utils;

namespace ToDoFlow.Services.Services.Interface
{
    public interface ITaskItemService
    {
        public Task<ApiResponse<TaskItemReadDto>> CreateTaskItemAsync(int userId, TaskItemCreateDto taskItemCreateDto);
        public Task<ApiResponse<List<TaskItemReadDto>>> ReadTaskItemAsync();
        public Task<ApiResponse<List<TaskItemReadDto>>> ReadTaskItemByUserAsync(int userId);
        public Task<ApiResponse<List<TaskItemReadDto>>> ReadTaskItemByCategoryAsync(int categoryId, int userId);
        public Task<ApiResponse<TaskItemReadDto>> ReadTaskItemByIdAsync(int id, int userId);
        public Task<ApiResponse<TaskItemReadDto>> UpdateTaskItemAsync(int id, int userId, TaskItemUpdateDto taskItemUpdateDto);
        public Task<ApiResponse> DeleteTaskItemAsync(int id, int userId);
    }
}
