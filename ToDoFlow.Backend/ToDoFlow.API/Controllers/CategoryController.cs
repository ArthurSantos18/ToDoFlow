using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.API.Controllers
{
    [Route("api/[controller]")]
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
        
        [HttpGet("UserId")]
        public async Task<ActionResult> ReadCategoryByUserAsync(int userId)
        {
            return Ok(await _categoryService.ReadCategoryByUserAsync(userId));
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
        public async Task<ActionResult> DeleteCategoryAsync(int id, int userId)
        {
            return Ok(await _categoryService.DeleteCategoryAsync(id, userId));
        }
    }
}
