using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Repositories.Interface
{
    public interface ITaskItemRepository
    {
        public Task<List<TaskItem>> CreateTaskItemAsync(TaskItem taskItem);
        public Task<List<TaskItem>> ReadTaskItemAsync();
        public Task<List<TaskItem>> ReadTaskItemByCategoryAsync(int categoryId);
        public Task<TaskItem> ReadTaskItemByIdAsync(int id);
        public Task<List<TaskItem>> UpdateTaskItemAsync(TaskItem taskItem);
        public Task<List<TaskItem>> DeleteTaskItemAsync(int id, int categoryId);
    }
}
