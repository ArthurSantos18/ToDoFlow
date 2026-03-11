using System.Threading.Tasks;
using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Repositories.Interface
{
    public interface ICategoryRepository
    {
        public Task<Category> CreateCategoryAsync(Category category);
        public Task<IEnumerable<Category>> GetCategoryAsync();
        public Task<IEnumerable<Category>> GetCategoryByUserAsync(int userId);
        public Task<Category> GetCategoryByIdAsync(int id);
        public Task<Category> UpdateCategoryAsync(Category category);
        public Task<bool> DeleteCategoryAsync(int userId);
    }
}
