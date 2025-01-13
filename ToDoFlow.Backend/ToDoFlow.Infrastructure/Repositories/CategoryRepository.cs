using Microsoft.EntityFrameworkCore;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Context;
using ToDoFlow.Infrastructure.Repositories.Interface;

namespace ToDoFlow.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ToDoFlowContext _context;

        public CategoryRepository(ToDoFlowContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> CreateCategoryAsync(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();

            return await _context.Categories.Include(t => t.Tasks).ToListAsync();
        }

        public async Task<List<Category>> ReadCategoryAsync()
        {
            return await _context.Categories.Include(t => t.Tasks).ToListAsync();
        }

        public async Task<Category> ReadCategoryAsync(int id)
        {
            return await _context.Categories.Include(t => t.Tasks).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Category>> UpdateCategoryAsync(Category category)
        {

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return await _context.Categories.Include(t => t.Tasks).ToListAsync();
        }

        public async Task<List<Category>> DeleteCategoryAsync(int id)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return await _context.Categories.Include(t => t.Tasks).ToListAsync(); ;
        }

    }
}
