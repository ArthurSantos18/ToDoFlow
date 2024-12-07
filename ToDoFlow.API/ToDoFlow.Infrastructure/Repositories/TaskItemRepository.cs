using Microsoft.EntityFrameworkCore;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Context;
using ToDoFlow.Infrastructure.Repositories.Interface;

namespace ToDoFlow.Infrastructure.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly ToDoFlowContext _context;

        public TaskItemRepository(ToDoFlowContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> CreateTaskItemAsync(TaskItem taskItem)
        {
            await _context.AddAsync(taskItem);
            await _context.SaveChangesAsync();

            return await _context.Tasks.ToListAsync();
        }

        public async Task<List<TaskItem>> ReadTaskItemAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem> ReadTaskItemAsync(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TaskItem>> UpdateTaskItemAsync(TaskItem taskItem)
        {
            _context.Tasks.Update(taskItem);
            await _context.SaveChangesAsync();

            return await _context.Tasks.ToListAsync();
        }

        public async Task<List<TaskItem>> DeleteTaskItemAsync(int id)
        {
            TaskItem taskItem = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
            _context.Tasks.Remove(taskItem);
            await _context.SaveChangesAsync();

            return await _context.Tasks.ToListAsync();
        }
    }
}
