using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Repositories.Interface
{
    public interface ITaskItemRepository
    {
        public Task<TaskItem> CreateTaskItemAsync(TaskItem taskItem);
        public Task<IEnumerable<TaskItem>> GetTaskItemAsync();
        public Task<IEnumerable<TaskItem>> GetTaskItemByUserAsync(int userId);
        public Task<IEnumerable<TaskItem>> GetTaskItemByCategoryAsync(int categoryId);
        public Task<TaskItem> GetTaskItemByIdAsync(int id);
        public Task<TaskItem> UpdateTaskItemAsync(TaskItem taskItem);
        public Task<bool> DeleteTaskItemAsync(int id);
    }
}
