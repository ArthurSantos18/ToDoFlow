using ToDoFlow.Application.Dtos;

namespace ToDoFlow.Services.Services.Interface
{
    public interface ITaskItemService
    {
        public Task<ApiResponse<List<TaskItemReadDto>>> CreateTaskItemAsync(TaskItemCreateDto taskItemCreateDto);
        public Task<ApiResponse<List<TaskItemReadDto>>> ReadTaskItemAsync();
        public Task<ApiResponse<List<TaskItemReadDto>>> ReadTaskItemByUserAsync(int userId);
        public Task<ApiResponse<List<TaskItemReadDto>>> ReadTaskItemByCategoryAsync(int categoryId);
        public Task<ApiResponse<TaskItemReadDto>> ReadTaskItemByIdAsync(int id);
        public Task<ApiResponse<List<TaskItemReadDto>>> UpdateTaskItemAsync(int id, TaskItemUpdateDto taskItemUpdateDto);
        public Task<ApiResponse<List<TaskItemReadDto>>> DeleteTaskItemAsync(int id, int categoryId);
    }
}
