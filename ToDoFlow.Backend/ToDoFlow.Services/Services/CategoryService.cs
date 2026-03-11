using AutoMapper;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Repositories.Interface;
using ToDoFlow.Services.Services.Interface;
using ToDoFlow.Services.Services.Utils;

namespace ToDoFlow.Services.Services
{
    public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<CategoryReadDto>> CreateCategoryAsync(int userId, CategoryCreateDto categoryCreateDto)
        {
            try
            {
                Category category = _mapper.Map<Category>(categoryCreateDto);
                category.UserId = userId;

                await _categoryRepository.CreateCategoryAsync(category);

                CategoryReadDto categoryReadDto = _mapper.Map<CategoryReadDto>(category);

                return new ApiResponse<CategoryReadDto>(categoryReadDto, true, "Category created successfully", 201);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CategoryReadDto>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<IEnumerable<CategoryReadDto>>> GetCategoryAsync()
        {
            try
            {
                IEnumerable<Category> categories = await _categoryRepository.GetCategoryAsync();
                IEnumerable<CategoryReadDto> categoryReadDtos = _mapper.Map<IEnumerable<CategoryReadDto>>(categories);

                return new ApiResponse<IEnumerable<CategoryReadDto>>(categoryReadDtos, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<CategoryReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<IEnumerable<CategoryReadDto>>> GetCategoryByUserAsync(int userId)
        {
            try
            {
                IEnumerable<Category> categories = await _categoryRepository.GetCategoryByUserAsync(userId);
                IEnumerable<CategoryReadDto> categoryReadDtos = _mapper.Map<IEnumerable<CategoryReadDto>>(categories);

                return new ApiResponse<IEnumerable<CategoryReadDto>>(categoryReadDtos, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<CategoryReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<CategoryReadDto>> GetCategoryByIdAsync(int id, int userId)
        {
            try
            {
                Category category = await _categoryRepository.GetCategoryByIdAsync(id);
                
                if (category == null)
                {
                    return new ApiResponse<CategoryReadDto>(null, false, "Erro: Category not found", 404);
                }

                if (category.UserId != userId)
                {
                    return new ApiResponse<CategoryReadDto>(null, false, "Unauthorized", 403);
                }

                CategoryReadDto categoryReadDto = _mapper.Map<CategoryReadDto>(category);

                return new ApiResponse<CategoryReadDto>(categoryReadDto, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CategoryReadDto>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<CategoryReadDto>> UpdateCategoryAsync(int id, int userId, CategoryUpdateDto categoryUpdateDto)
        {
            try
            {
                Category category = await _categoryRepository.GetCategoryByIdAsync(id);

                if (category == null)
                {
                    return new ApiResponse<CategoryReadDto>(null, false, "Category not found", 404);
                }

                if (category.UserId != userId)
                {
                    return new ApiResponse<CategoryReadDto>(null, false, "Unauthorized", 403);
                }
                
                _mapper.Map(categoryUpdateDto, category);
                await _categoryRepository.UpdateCategoryAsync(category);

                
                CategoryReadDto categoryReadDto = _mapper.Map<CategoryReadDto>(category);

                return new ApiResponse<CategoryReadDto>(categoryReadDto, true, "Category updated successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CategoryReadDto>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse> DeleteCategoryAsync(int id, int userId)
        {
            try
            {
                Category category = await _categoryRepository.GetCategoryByIdAsync(id);

                if (category == null)
                {
                    return new ApiResponse(false, "Category not found", 404);
                }

                if (category.UserId != userId)
                {
                    return new ApiResponse(false, "Unauthorized", 403);
                }

                await _categoryRepository.DeleteCategoryAsync(id);
                

                return new ApiResponse(true, "Category deleted successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, $"Erro: {ex.Message}", 500);
            }
        }
    }
}
