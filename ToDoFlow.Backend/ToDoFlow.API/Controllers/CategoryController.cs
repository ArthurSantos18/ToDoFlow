using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            int userId = GetCurrentUserId();

            return Ok(await _categoryService.CreateCategoryAsync(userId, categoryCreateDto));
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> ReadCategoryAsync()
        {
            return Ok(await _categoryService.ReadCategoryAsync());
        }

        [HttpGet("user/{userId:int}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> ReadCategoryForUserByAdminAsync(int userId)
        {
            return Ok(await _categoryService.ReadCategoryByUserAsync(userId));
        }

        [HttpGet("me")]
        public async Task<ActionResult> ReadCategoryByUserAsync()
        {
            int userId = GetCurrentUserId();

            return Ok(await _categoryService.ReadCategoryByUserAsync(userId));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> ReadCategoryByIdAsync(int id)
        {
            int userId = GetCurrentUserId();

            return Ok(await _categoryService.ReadCategoryByIdAsync(id, userId));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateDto)
        {
            int userId = GetCurrentUserId();

            return Ok(await _categoryService.UpdateCategoryAsync(id, userId, categoryUpdateDto));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            int userId = GetCurrentUserId();

            return Ok(await _categoryService.DeleteCategoryAsync(id, userId));
        }
    }
}
