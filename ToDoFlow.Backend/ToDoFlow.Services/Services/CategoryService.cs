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

        public async Task<ApiResponse<List<CategoryReadDto>>> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            try
            {
                Category category = _mapper.Map<Category>(categoryCreateDto);
                await _categoryRepository.CreateCategoryAsync(category);

                List<Category> categories = await _categoryRepository.ReadCategoryByUserAsync(category.UserId);
                List<CategoryReadDto> categoryReadDtos = _mapper.Map<List<CategoryReadDto>>(categories);

                return new ApiResponse<List<CategoryReadDto>>(categoryReadDtos, true, "Category created successfully", 201);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CategoryReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<CategoryReadDto>>> ReadCategoryAsync()
        {
            try
            {
                List<Category> categories = await _categoryRepository.ReadCategoryAsync();
                List<CategoryReadDto> categoryReadDtos = _mapper.Map<List<CategoryReadDto>>(categories);

                return new ApiResponse<List<CategoryReadDto>>(categoryReadDtos, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CategoryReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<CategoryReadDto>>> ReadCategoryByUserAsync(int userId)
        {
            try
            {
                List<Category> categories = await _categoryRepository.ReadCategoryByUserAsync(userId);
                List<CategoryReadDto> categoryReadDtos = _mapper.Map<List<CategoryReadDto>>(categories);

                return new ApiResponse<List<CategoryReadDto>>(categoryReadDtos, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CategoryReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<CategoryReadDto>> ReadCategoryByIdAsync(int id)
        {
            try
            {
                Category category = await _categoryRepository.ReadCategoryByIdAsync(id);
                CategoryReadDto categoryReadDto = _mapper.Map<CategoryReadDto>(category);

                return new ApiResponse<CategoryReadDto>(categoryReadDto, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CategoryReadDto>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<CategoryReadDto>>> UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateDto)
        {
            try
            {
                Category category = await _categoryRepository.ReadCategoryByIdAsync(id);
                _mapper.Map(categoryUpdateDto, category);
                await _categoryRepository.UpdateCategoryAsync(category);

                List<Category> categories = await _categoryRepository.ReadCategoryByUserAsync(category.UserId);
                List<CategoryReadDto> categoryReadDtos = _mapper.Map<List<CategoryReadDto>>(categories);

                return new ApiResponse<List<CategoryReadDto>>(categoryReadDtos, true, "Category updated successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CategoryReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<CategoryReadDto>>> DeleteCategoryAsync(int id)
        {
            try
            {
                List<Category> categories = await _categoryRepository.DeleteCategoryAsync(id);
                List<CategoryReadDto> categoryReadDtos = _mapper.Map<List<CategoryReadDto>>(categories);

                return new ApiResponse<List<CategoryReadDto>>(categoryReadDtos, true, "Category deleted successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CategoryReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }
    }
}
