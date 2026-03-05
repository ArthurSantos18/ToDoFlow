using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Services.Services.Interface;
using ToDoFlow.Services.Services.Utils;

namespace ToDoFlow.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

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
        [Authorize (Roles = "Administrator")]
        public async Task<ActionResult<ApiResponse<List<UserReadDto>>>> CreateUserAdminAsync(UserCreateDto userCreateDto)
        {
            var response = await _userService.CreateUserAsync(userCreateDto);

            return StatusCode(response.HttpStatus, response);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<ApiResponse<List<UserReadDto>>>> ReadUserAsync()
        {
            var response = await _userService.ReadUserAsync();

            return StatusCode(response.HttpStatus, response);
        }

        [HttpGet("me")]
        public async Task<ActionResult<ApiResponse<UserReadDto>>> ReadUserByIdAsync()
        {
            int userId = GetCurrentUserId();

            var response = await _userService.ReadUserByIdAsync(userId);

            return StatusCode(response.HttpStatus, response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse<UserReadDto>>> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            int userId = GetCurrentUserId();

            var response = await _userService.UpdateUserAsync(userId, userUpdateDto);

            return StatusCode(response.HttpStatus, response);

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<ApiResponse<List<UserReadDto>>>> DeleteUserAsync(int id)
        {
            int userId = GetCurrentUserId();

            var response = await _userService.DeleteUserAsync(id);

            return StatusCode(response.HttpStatus, response);
        }
    }
}
