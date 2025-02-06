using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    //[Authorize]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        [HttpPost]
        public async Task<ActionResult> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            return Ok(await _categoryService.CreateCategoryAsync(categoryCreateDto));
        }

        [HttpGet]
        public async Task<ActionResult> ReadCategoryAsync()
        {
            return Ok(await _categoryService.ReadCategoryAsync());
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult> ReadCategoryByUserAsync(int userId)
        {
            return Ok(await _categoryService.ReadCategoryByUserAsync(userId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> ReadCategoryByIdAsync(int id)
        {
            return Ok(await _categoryService.ReadCategoryByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateDto)
        {
            return Ok(await _categoryService.UpdateCategoryAsync(id, categoryUpdateDto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            return Ok(await _categoryService.DeleteCategoryAsync(id));
        }
    }
}
