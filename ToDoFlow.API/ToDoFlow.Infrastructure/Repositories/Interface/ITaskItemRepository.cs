using ToDoFlow.Application.Dtos;
using ToDoFlow.Services.Services;

namespace ToDoFlow.Infrastructure.Repositories.Interface
{
    public interface ITaskItemRepository
    {
        Task<ApiResponse<List<TaskItemReadDto>>> CreateTaskItemAsync(TaskItemCreateDto TaskItemCreateDto);
        Task<ApiResponse<List<TaskItemReadDto>>> ReadTaskItemAsync();
        Task<ApiResponse<TaskItemReadDto>> ReadTaskItemAsync(int id);
        Task<ApiResponse<List<TaskItemReadDto>>> UpdateTaskItemAsync(TaskItemUpdateDto taskItemUpdateDto);
        Task<ApiResponse<List<TaskItemReadDto>>> DeleteTaskItemAsync(int id);
    }
}
