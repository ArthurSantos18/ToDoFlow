using ToDoFlow.Application.Dtos;

namespace ToDoFlow.Services.Services.Interface
{
    public interface ICategoryService
    {
        public Task<ApiResponse<List<CategoryReadDto>>> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
        public Task<ApiResponse<List<CategoryReadDto>>> ReadCategoryAsync();
        public Task<ApiResponse<CategoryReadDto>> ReadCategoryAsync(int id);
        public Task<ApiResponse<List<CategoryReadDto>>> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto);
        public Task<ApiResponse<List<CategoryReadDto>>> DeleteCategoryAsync(int id);
    }
}
