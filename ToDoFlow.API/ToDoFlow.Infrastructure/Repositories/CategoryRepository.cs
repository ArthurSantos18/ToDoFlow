using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Context;
using ToDoFlow.Infrastructure.Repositories.Interface;
using ToDoFlow.Services.Services;

namespace ToDoFlow.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ToDoFlowContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(ToDoFlowContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<CategoryReadDto>>> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            try
            {
                Category category = _mapper.Map<Category>(categoryCreateDto);
                await _context.AddAsync(category);
                await _context.SaveChangesAsync();

                List<Category> categories = await _context.Categories.Include(t => t.Tasks).ToListAsync();
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
                List<Category> categories = await _context.Categories.Include(t => t.Tasks).ToListAsync();
                List<CategoryReadDto> categoryReadDtos = _mapper.Map<List<CategoryReadDto>>(categories);

                return new ApiResponse<List<CategoryReadDto>>(categoryReadDtos, true, "Operação concluída com sucesso", 200);
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
                Category category = await _context.Categories.Include(t => t.Tasks).FirstOrDefaultAsync(c => c.Id == id);
                
                if (category == null)
                {
                    return new ApiResponse<CategoryReadDto>(null, true, $"Erro: Categoria não encontrada", 404);
                }

                CategoryReadDto categoryReadDto = _mapper.Map<CategoryReadDto>(category);

                return new ApiResponse<CategoryReadDto>(categoryReadDto, true, "Operação concluída com sucesso", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<CategoryReadDto>(null, true, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<CategoryReadDto>>> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
        {
            try
            {
                Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryUpdateDto.Id);
                _mapper.Map(categoryUpdateDto, category);
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();

                List<Category> categories = await _context.Categories.Include(t => t.Tasks).ToListAsync();
                List<CategoryReadDto> categoryReadDtos = _mapper.Map<List<CategoryReadDto>>(categories);

                return new ApiResponse<List<CategoryReadDto>>(categoryReadDtos, true, "Categoria deletada com sucesso", 204);
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
                Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                List<Category> categories = await _context.Categories.Include(t => t.Tasks).ToListAsync();
                List<CategoryReadDto> categoryReadDtos = _mapper.Map<List<CategoryReadDto>>(categories);

                return new ApiResponse<List<CategoryReadDto>>(categoryReadDtos, true, "Categoria deletada com sucesso", 204);

            }
            catch (Exception ex)
            {
                return new ApiResponse<List<CategoryReadDto>>(null, false, $"Erro: {ex.InnerException}", 500);
            }
        }

    }
}
