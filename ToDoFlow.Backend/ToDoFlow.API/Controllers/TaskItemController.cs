using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.API.Controllers
{
    [Route("api/taskitems")]
    [ApiController]
    //[Authorize]
    public class TaskItemController(ITaskItemService taskItemService) : ControllerBase
    {
        private readonly ITaskItemService _taskItemService = taskItemService;

        [HttpPost]
        public async Task<ActionResult> CreateTaskItemAsync(TaskItemCreateDto taskItemCreateDto)
        {
            return Ok(await _taskItemService.CreateTaskItemAsync(taskItemCreateDto));
        }

        [HttpGet]
        public async Task<ActionResult> ReadTaskItemAsync()
        {
            return Ok(await _taskItemService.ReadTaskItemAsync());
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult> ReadTaskItemByCategoryAsync(int categoryId)
        {
            return Ok(await _taskItemService.ReadTaskItemByCategoryAsync(categoryId));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> ReadTaskItemByIdAsync(int id)
        {
            return Ok(await _taskItemService.ReadTaskItemByIdAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTaskItemAsync(int id, TaskItemUpdateDto taskItemUpdateDto)
        {
            return Ok(await _taskItemService.UpdateTaskItemAsync(id, taskItemUpdateDto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTaskItemAsync(int id, int categoryId)
        {
            return Ok(await _taskItemService.DeleteTaskItemAsync(id, categoryId));
        }
    }
}
