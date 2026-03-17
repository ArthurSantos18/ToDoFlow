using ToDoFlow.Application.Dtos;
using ToDoFlow.Application.Services.Utils;

namespace ToDoFlow.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<ApiResponse<CategoryReadDto>> CreateCategoryAsync(int userId, CategoryCreateDto categoryCreateDto);
        public Task<ApiResponse<IEnumerable<CategoryReadDto>>> GetCategoryAsync();
        public Task<ApiResponse<IEnumerable<CategoryReadDto>>> GetCategoryByUserAsync(int userId);
        public Task<ApiResponse<CategoryReadDto>> GetCategoryByIdAsync(int id, int userId);
        public Task<ApiResponse<CategoryReadDto>> UpdateCategoryAsync(int id, int userId, CategoryUpdateDto categoryUpdateDto);
        public Task<ApiResponse> DeleteCategoryAsync(int id, int userId);
    }
}
