using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Infrastructure.Repositories.Interface;

namespace ToDoFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            return Ok(await _categoryRepository.CreateCategoryAsync(categoryCreateDto));
        }
        
        [HttpGet]
        public async Task<ActionResult> ReadCategoryAsync()
        {
            return Ok(await _categoryRepository.ReadCategoryAsync());
        }

        [HttpGet("Id")]
        public async Task<ActionResult> ReadCategoryAsync(int id)
        {
            return Ok(await _categoryRepository.ReadCategoryAsync(id));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
        {
            return Ok(await _categoryRepository.UpdateCategoryAsync(categoryUpdateDto));
        }

        [HttpDelete("Id")]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            return Ok(await _categoryRepository.DeleteCategoryAsync(id));
        }
    }
}
