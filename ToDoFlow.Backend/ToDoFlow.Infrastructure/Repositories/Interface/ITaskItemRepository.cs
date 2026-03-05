using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Repositories.Interface
{
    public interface ITaskItemRepository
    {
        public Task<TaskItem> CreateTaskItemAsync(TaskItem taskItem);
        public Task<List<TaskItem>> ReadTaskItemAsync();
        public Task<List<TaskItem>> ReadTaskItemByUserAsync(int userId);
        public Task<List<TaskItem>> ReadTaskItemByCategoryAsync(int categoryId);
        public Task<TaskItem> ReadTaskItemByIdAsync(int id);
        public Task<TaskItem> UpdateTaskItemAsync(TaskItem taskItem);
        public Task<bool> DeleteTaskItemAsync(int id);
    }
}
