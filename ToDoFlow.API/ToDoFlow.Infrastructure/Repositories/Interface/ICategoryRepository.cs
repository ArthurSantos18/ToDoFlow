using ToDoFlow.Application.Dtos;
using ToDoFlow.Services.Services;

namespace ToDoFlow.Infrastructure.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<ApiResponse<List<CategoryReadDto>>> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
        Task<ApiResponse<List<CategoryReadDto>>> ReadCategoryAsync();
        Task<ApiResponse<CategoryReadDto>> ReadCategoryAsync(int id);
        Task<ApiResponse<List<CategoryReadDto>>> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto);
        Task<ApiResponse<List<CategoryReadDto>>> DeleteCategoryAsync(int id);
    }
}
