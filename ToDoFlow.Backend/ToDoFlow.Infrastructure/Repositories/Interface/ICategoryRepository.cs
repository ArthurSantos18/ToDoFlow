using System.Threading.Tasks;
using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Repositories.Interface
{
    public interface ICategoryRepository
    {
        public Task<Category> CreateCategoryAsync(Category category);
        public Task<List<Category>> ReadCategoryAsync();
        public Task<List<Category>> ReadCategoryByUserAsync(int userId);
        public Task<Category> ReadCategoryByIdAsync(int id);
        public Task<Category> UpdateCategoryAsync(Category category);
        public Task<bool> DeleteCategoryAsync(int userId);
    }
}
