using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
using ToDoFlow.Services.Services.Interface;

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
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        } 

        [HttpPost]
        public async Task<ActionResult> CreateTaskItemAsync(TaskItemCreateDto taskItemCreateDto)
        {
            int userId = GetCurrentUserId();

            return Ok(await _taskItemService.CreateTaskItemAsync(userId, taskItemCreateDto));
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> ReadTaskItemAsync()
        {
            return Ok(await _taskItemService.ReadTaskItemAsync());
        }

        [HttpGet("user/{userId:int}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> ReadTaskItemForUserByAdminAsync(int userId)
        {
            return Ok(await _taskItemService.ReadTaskItemByUserAsync(userId));
        }

        [HttpGet("me")]
        public async Task<ActionResult> ReadTaskItemByUserAsync()
        {
            int userId = GetCurrentUserId();

            return Ok(await _taskItemService.ReadTaskItemByUserAsync(userId));
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult> ReadTaskItemByCategoryAsync(int categoryId)
        {
            int userId = GetCurrentUserId();
            
            return Ok(await _taskItemService.ReadTaskItemByCategoryAsync(categoryId, userId));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> ReadTaskItemByIdAsync(int id)
        {
            int userId = GetCurrentUserId();

            return Ok(await _taskItemService.ReadTaskItemByIdAsync(id, userId));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateTaskItemAsync(int id, TaskItemUpdateDto taskItemUpdateDto)
        {
            int userId = GetCurrentUserId();

            return Ok(await _taskItemService.UpdateTaskItemAsync(id, userId, taskItemUpdateDto));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTaskItemAsync(int id)
        {
            int userId = GetCurrentUserId();

            return Ok(await _taskItemService.DeleteTaskItemAsync(id, userId));
        }
    }
}
