using Microsoft.EntityFrameworkCore;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Context;
using ToDoFlow.Infrastructure.Repositories.Interface;

namespace ToDoFlow.Infrastructure.Repositories
{
    public class CategoryRepository(ToDoFlowContext context) : ICategoryRepository
    {
        private readonly ToDoFlowContext _context = context;

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<List<Category>> GetCategoryAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<List<Category>> GetCategoryByUserAsync(int userId)
        {
            return await _context.Categories.Where(u => u.UserId == userId).ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) {
                return false;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
