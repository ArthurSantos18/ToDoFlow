using AutoMapper;
using System.ComponentModel.DataAnnotations;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Application.Services.Interface;
using ToDoFlow.Application.Services.Utils;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Repositories.Interface;

namespace ToDoFlow.Application.Services
{
    public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<CategoryReadDto>> CreateCategoryAsync(int userId, CategoryCreateDto categoryCreateDto)
        {
            
            Category category = _mapper.Map<Category>(categoryCreateDto);
            category.UserId = userId;

            await _categoryRepository.CreateCategoryAsync(category);

            CategoryReadDto categoryReadDto = _mapper.Map<CategoryReadDto>(category);

            return new ApiResponse<CategoryReadDto>(categoryReadDto, true, "Category created successfully", 201);
        }

        public async Task<ApiResponse<IEnumerable<CategoryReadDto>>> GetCategoryAsync()
        {
            
            IEnumerable<Category> categories = await _categoryRepository.GetCategoryAsync();
            IEnumerable<CategoryReadDto> categoryReadDtos = _mapper.Map<IEnumerable<CategoryReadDto>>(categories);

            return new ApiResponse<IEnumerable<CategoryReadDto>>(categoryReadDtos, true, "Operation carried out successfully", 200);
        }

        public async Task<ApiResponse<IEnumerable<CategoryReadDto>>> GetCategoryByUserAsync(int userId)
        {
            if (userId < 0)
            {
                throw new ArgumentException($"Invalid user ID: {userId}");
            }
                
            IEnumerable<Category> categories = await _categoryRepository.GetCategoryByUserAsync(userId);
            IEnumerable<CategoryReadDto> categoryReadDtos = _mapper.Map<IEnumerable<CategoryReadDto>>(categories);

            return new ApiResponse<IEnumerable<CategoryReadDto>>(categoryReadDtos, true, "Operation carried out successfully", 200);
        }

        public async Task<ApiResponse<CategoryReadDto>> GetCategoryByIdAsync(int id, int userId)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Invalid category ID: {id}");
            }
                
            if (userId < 0)
            {
                throw new ArgumentException($"Invalid user ID: {userId}");
            }
                
            Category category = await _categoryRepository.GetCategoryByIdAsync(id);
                
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found");
            }

            if (category.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to access this category");

            }

            CategoryReadDto categoryReadDto = _mapper.Map<CategoryReadDto>(category);

            return new ApiResponse<CategoryReadDto>(categoryReadDto, true, "Operation carried out successfully", 200);
        }

        public async Task<ApiResponse<CategoryReadDto>> UpdateCategoryAsync(int id, int userId, CategoryUpdateDto categoryUpdateDto)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Invalid category ID: {id}");
            }
                
            if (userId < 0)
            {
                throw new ArgumentException($"Invalid user ID: {id}");
            }
                
            Category category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found");
            }

            if (category.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to delete this category");
            }
                
            _mapper.Map(categoryUpdateDto, category);
            await _categoryRepository.UpdateCategoryAsync(category);

                
            CategoryReadDto categoryReadDto = _mapper.Map<CategoryReadDto>(category);

            return new ApiResponse<CategoryReadDto>(categoryReadDto, true, "Category updated successfully", 200);
        }

        public async Task<ApiResponse> DeleteCategoryAsync(int id, int userId)
        {
            Category category = await _categoryRepository.GetCategoryByIdAsync(id);

            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found");
            }

            if (category.UserId != userId)
            {
                throw new UnauthorizedAccessException("You do not have permission to delete this category");
            }

            await _categoryRepository.DeleteCategoryAsync(id);
                
            return new ApiResponse(true, "Category deleted successfully", 200);
        }
    }
}
