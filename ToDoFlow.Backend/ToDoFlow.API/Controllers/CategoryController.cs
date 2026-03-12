using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Application.Services.Interface;
using ToDoFlow.Application.Services.Utils;

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
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
            {
                throw new UnauthorizedAccessException("Id not found");
            }

            return int.Parse(userIdClaim.Value);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<List<CategoryReadDto>>>> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            int userId = GetCurrentUserId();

            var response = await _categoryService.CreateCategoryAsync(userId, categoryCreateDto);

            return StatusCode(response.HttpStatus, response);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<ApiResponse<List<CategoryReadDto>>>> GetCategoryAsync()
        {
            var response = await _categoryService.GetCategoryAsync();

            return StatusCode(response.HttpStatus, response);
        }

        [HttpGet("user/{userId:int}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<ApiResponse<List<CategoryReadDto>>>> GetCategoryForUserByAdminAsync(int userId)
        {
            var response = await _categoryService.GetCategoryByUserAsync(userId);

            return StatusCode(response.HttpStatus, response);
        }

        [HttpGet("me")]
        public async Task<ActionResult<ApiResponse<List<CategoryReadDto>>>> GetCategoryByUserAsync()
        {
            int userId = GetCurrentUserId();

            var response = await _categoryService.GetCategoryByUserAsync(userId);

            return StatusCode(response.HttpStatus, response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<CategoryReadDto>>> GetCategoryByIdAsync(int id)
        {
            int userId = GetCurrentUserId();

            var response = await _categoryService.GetCategoryByIdAsync(id, userId);

            return StatusCode(response.HttpStatus, response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<List<CategoryReadDto>>>> UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateDto)
        {
            int userId = GetCurrentUserId();

            var response = await _categoryService.UpdateCategoryAsync(id, userId, categoryUpdateDto);

            return StatusCode(response.HttpStatus, response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<List<CategoryReadDto>>>> DeleteCategoryAsync(int id)
        {
            int userId = GetCurrentUserId();

            var response = await _categoryService.DeleteCategoryAsync(id, userId);

            return StatusCode(response.HttpStatus, response);
        }
    }
}
