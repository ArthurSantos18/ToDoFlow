using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Repositories.Interface
{
    public interface ITaskItemRepository
    {
        public Task<TaskItem> CreateTaskItemAsync(TaskItem taskItem);
        public Task<List<TaskItem>> GetTaskItemAsync();
        public Task<List<TaskItem>> GetTaskItemByUserAsync(int userId);
        public Task<List<TaskItem>> GetTaskItemByCategoryAsync(int categoryId);
        public Task<TaskItem> GetTaskItemByIdAsync(int id);
        public Task<TaskItem> UpdateTaskItemAsync(TaskItem taskItem);
        public Task<bool> DeleteTaskItemAsync(int id);
    }
}
