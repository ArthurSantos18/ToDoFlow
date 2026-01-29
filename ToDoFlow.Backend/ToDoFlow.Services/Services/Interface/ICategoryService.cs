using ToDoFlow.Application.Dtos;
using ToDoFlow.Services.Services.Utils;

namespace ToDoFlow.Services.Services.Interface
{
    public interface ICategoryService
    {
        public Task<ApiResponse<List<CategoryReadDto>>> CreateCategoryAsync(int userId, CategoryCreateDto categoryCreateDto);
        public Task<ApiResponse<List<CategoryReadDto>>> ReadCategoryAsync();
        public Task<ApiResponse<List<CategoryReadDto>>> ReadCategoryByUserAsync(int userId);
        public Task<ApiResponse<CategoryReadDto>> ReadCategoryByIdAsync(int id, int userId);
        public Task<ApiResponse<List<CategoryReadDto>>> UpdateCategoryAsync(int id, int userId, CategoryUpdateDto categoryUpdateDto);
        public Task<ApiResponse<List<CategoryReadDto>>> DeleteCategoryAsync(int id, int userId);
    }
}
