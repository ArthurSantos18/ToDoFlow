using Microsoft.EntityFrameworkCore;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Context;
using ToDoFlow.Infrastructure.Repositories.Interface;

namespace ToDoFlow.Infrastructure.Repositories
{
    public class CategoryRepository(ToDoFlowContext context) : ICategoryRepository
    {
        private readonly ToDoFlowContext _context = context;

        public async Task<List<Category>> CreateCategoryAsync(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();

            return await _context.Categories.Where(u => u.UserId == category.UserId).ToListAsync();
        }

        public async Task<List<Category>> ReadCategoryByUserAsync(int userId)
        {
            return await _context.Categories.Where(u => u.UserId == userId).ToListAsync();
        }

        public async Task<Category> ReadCategoryAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Category>> UpdateCategoryAsync(Category category)
        {

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return await _context.Categories.Where(u => u.UserId == category.UserId).Include(t => t.Tasks).ToListAsync();
        }

        public async Task<List<Category>> DeleteCategoryAsync(int id, int userId)
        {
            _context.Categories.Remove(await _context.Categories.FirstOrDefaultAsync(c => c.Id == id));
            await _context.SaveChangesAsync();

            return await _context.Categories.Where(u => u.UserId == userId).ToListAsync();
        }

    }
}
