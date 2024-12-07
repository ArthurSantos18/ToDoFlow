using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Repositories.Interface
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> CreateCategoryAsync(Category category);
        public Task<List<Category>> ReadCategoryAsync();
        public Task<Category> ReadCategoryAsync(int id);
        public Task<List<Category>> UpdateCategoryAsync(Category category);
        public Task<List<Category>> DeleteCategoryAsync(int id);
    }
}
