using ToDoFlow.Application.Dtos;

namespace ToDoFlow.Services.Services.Interface
{
    public interface ITaskItemService
    {
        public Task<ApiResponse<List<TaskItemReadDto>>> CreateTaskItemAsync(TaskItemCreateDto taskItemCreateDto);
        public Task<ApiResponse<List<TaskItemReadDto>>> ReadTaskItemAsync();
        public Task<ApiResponse<TaskItemReadDto>> ReadTaskItemAsync(int id);
        public Task<ApiResponse<List<TaskItemReadDto>>> UpdateTaskItemAsync(TaskItemUpdateDto taskItemUpdateDto);
        public Task<ApiResponse<List<TaskItemReadDto>>> DeleteTaskItemAsync(int id);
    }
}
