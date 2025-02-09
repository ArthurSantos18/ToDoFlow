using Microsoft.EntityFrameworkCore;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Context;
using ToDoFlow.Infrastructure.Repositories.Interface;

namespace ToDoFlow.Infrastructure.Repositories
{
    public class TaskItemRepository(ToDoFlowContext context) : ITaskItemRepository
    {
        private readonly ToDoFlowContext _context = context;

        public async Task<List<TaskItem>> CreateTaskItemAsync(TaskItem taskItem)
        {
            await _context.AddAsync(taskItem);
            await _context.SaveChangesAsync();

            return await _context.Tasks.Where(t => t.CategoryId == taskItem.CategoryId).ToListAsync();
        }

        public async Task<List<TaskItem>> ReadTaskItemAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<List<TaskItem>> ReadTaskItemByCategoryAsync(int categoryId)
        {
            return await _context.Tasks.Where(t => t.CategoryId == categoryId).ToListAsync();
        }

        public async Task<List<TaskItem>> ReadTaskItemByUserAsync(int userId)
        {
            return await _context.Tasks.Where(t => 
            _context.Categories.Where(c => c.UserId == userId).Select(c => c.Id).Contains(t.CategoryId)).ToListAsync();
        }

        public async Task<TaskItem> ReadTaskItemByIdAsync(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TaskItem>> UpdateTaskItemAsync(TaskItem taskItem)
        {
            _context.Tasks.Update(taskItem);
            await _context.SaveChangesAsync();

            return await _context.Tasks.Where(t => t.CategoryId == taskItem.CategoryId).ToListAsync();
        }

        public async Task<List<TaskItem>> DeleteTaskItemAsync(int id, int categoryId)
        {
            _context.Tasks.Remove(await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id));
            await _context.SaveChangesAsync();

            return await _context.Tasks.Where(t => t.CategoryId == categoryId).ToListAsync();
        }
    }
}
