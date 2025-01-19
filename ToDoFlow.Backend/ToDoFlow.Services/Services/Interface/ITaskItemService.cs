using ToDoFlow.Application.Dtos;

namespace ToDoFlow.Services.Services.Interface
{
    public interface ITaskItemService
    {
        public Task<ApiResponse<List<TaskItemReadDto>>> CreateTaskItemAsync(TaskItemCreateDto taskItemCreateDto);
        public Task<ApiResponse<List<TaskItemReadDto>>> ReadTaskItemByCategoryAsync(int categoryId);
        public Task<ApiResponse<TaskItemReadDto>> ReadTaskItemAsync(int id);
        public Task<ApiResponse<List<TaskItemReadDto>>> UpdateTaskItemAsync(TaskItemUpdateDto taskItemUpdateDto);
        public Task<ApiResponse<List<TaskItemReadDto>>> DeleteTaskItemAsync(int id, int categoryId);
    }
}
