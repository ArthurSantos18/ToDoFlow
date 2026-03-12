using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Application.Services.Interface;
using ToDoFlow.Application.Services.Utils;

namespace ToDoFlow.API.Controllers
{
    [Route("api/taskitems")]
    [ApiController]
    [Authorize]
    public class TaskItemController(ITaskItemService taskItemService) : ControllerBase
    {
        private readonly ITaskItemService _taskItemService = taskItemService;

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
        public async Task<ActionResult<ApiResponse<List<TaskItemReadDto>>>> CreateTaskItemAsync(TaskItemCreateDto taskItemCreateDto)
        {
            int userId = GetCurrentUserId();

            var response = await _taskItemService.CreateTaskItemAsync(userId, taskItemCreateDto);

            return StatusCode(response.HttpStatus, response);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<ApiResponse<List<TaskItemReadDto>>>> GetTaskItemAsync()
        {
            var response = await _taskItemService.GetTaskItemAsync();

            return StatusCode(response.HttpStatus, response);
        }

        [HttpGet("user/{userId:int}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<ApiResponse<List<TaskItemReadDto>>>> GetTaskItemForUserByAdminAsync(int userId)
        {
            var response = await _taskItemService.GetTaskItemByUserAsync(userId);
            
            return StatusCode(response.HttpStatus, response);
        }

        [HttpGet("me")]
        public async Task<ActionResult<ApiResponse<List<TaskItemReadDto>>>> GetTaskItemByUserAsync()
        {
            int userId = GetCurrentUserId();

            var response = await _taskItemService.GetTaskItemByUserAsync(userId);

            return StatusCode(response.HttpStatus, response);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<ApiResponse<List<TaskItemReadDto>>>> GetTaskItemByCategoryAsync(int categoryId)
        {
            int userId = GetCurrentUserId();
            
            var response = await _taskItemService.GetTaskItemByCategoryAsync(categoryId, userId);

            return StatusCode(response.HttpStatus, response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<TaskItemReadDto>>> GetTaskItemByIdAsync(int id)
        {
            int userId = GetCurrentUserId();

            var response = await _taskItemService.GetTaskItemByIdAsync(id, userId);

            return StatusCode(response.HttpStatus, response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ApiResponse<List<TaskItemReadDto>>>> UpdateTaskItemAsync(int id, TaskItemUpdateDto taskItemUpdateDto)
        {
            int userId = GetCurrentUserId();

            var response = await _taskItemService.UpdateTaskItemAsync(id, userId, taskItemUpdateDto);

            return StatusCode(response.HttpStatus, response);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ApiResponse<List<TaskItemReadDto>>>> DeleteTaskItemAsync(int id)
        {
            int userId = GetCurrentUserId();

            var response = await _taskItemService.DeleteTaskItemAsync(id, userId);

            return StatusCode(response.HttpStatus, response);
        }
    }
}
