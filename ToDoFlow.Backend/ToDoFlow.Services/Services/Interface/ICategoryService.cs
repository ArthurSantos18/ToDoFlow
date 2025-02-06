using ToDoFlow.Application.Dtos;

namespace ToDoFlow.Services.Services.Interface
{
    public interface ICategoryService
    {
        public Task<ApiResponse<List<CategoryReadDto>>> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
        public Task<ApiResponse<List<CategoryReadDto>>> ReadCategoryAsync();
        public Task<ApiResponse<List<CategoryReadDto>>> ReadCategoryByUserAsync(int userId);
        public Task<ApiResponse<CategoryReadDto>> ReadCategoryByIdAsync(int id);
        public Task<ApiResponse<List<CategoryReadDto>>> UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateDto);
        public Task<ApiResponse<List<CategoryReadDto>>> DeleteCategoryAsync(int id);
    }
}
