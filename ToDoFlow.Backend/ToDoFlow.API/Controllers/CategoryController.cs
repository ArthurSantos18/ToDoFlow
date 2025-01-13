using Microsoft.AspNetCore.Mvc;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

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

        [HttpGet("Id")]
        public async Task<ActionResult> ReadCategoryAsync(int id)
        {
            return Ok(await _categoryService.ReadCategoryAsync(id));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
        {
            return Ok(await _categoryService.UpdateCategoryAsync(categoryUpdateDto));
        }

        [HttpDelete("Id")]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            return Ok(await _categoryService.DeleteCategoryAsync(id));
        }
    }
}
