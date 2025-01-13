using AutoMapper;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Repositories.Interface;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<CategoryReadDto>>> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            try
            {
                Category category = _mapper.Map<Category>(categoryCreateDto);
                await _categoryRepository.CreateCategoryAsync(category);

                List<Category> categories = await _categoryRepository.ReadCategoryAsync();
                List<CategoryReadDto> categoryReadDtos = _mapper.Map<List<CategoryReadDto>>(categories);

                return new ApiResponse<List<CategoryReadDto>>(categoryReadDtos, true, "Categoria criada com sucesso", 201);
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

                return new ApiResponse<List<CategoryReadDto>>(categoryReadDtos, true, "Operação realizada com suceso", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CategoryReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<CategoryReadDto>> ReadCategoryAsync(int id)
        {
            try
            {
                Category category = await _categoryRepository.ReadCategoryAsync(id);
                CategoryReadDto categoryReadDto = _mapper.Map<CategoryReadDto>(category);

                return new ApiResponse<CategoryReadDto>(categoryReadDto, true, "Operação realizada com sucesso", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CategoryReadDto>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<CategoryReadDto>>> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
        {
            try
            {
                Category category = await _categoryRepository.ReadCategoryAsync(categoryUpdateDto.Id);
                _mapper.Map(categoryUpdateDto, category);
                await _categoryRepository.UpdateCategoryAsync(category);

                List<Category> categories = await _categoryRepository.ReadCategoryAsync();
                List<CategoryReadDto> categoryReadDtos = _mapper.Map<List<CategoryReadDto>>(categories);

                return new ApiResponse<List<CategoryReadDto>>(categoryReadDtos, true, "Categoria atualizada com suceso", 200);
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
                await _categoryRepository.DeleteCategoryAsync(id);

                List<Category> categories = await _categoryRepository.ReadCategoryAsync();
                List<CategoryReadDto> categoryReadDtos = _mapper.Map<List<CategoryReadDto>>(categories);

                return new ApiResponse<List<CategoryReadDto>>(categoryReadDtos, true, "Categoria deletada com suceso", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CategoryReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }
    }
}
