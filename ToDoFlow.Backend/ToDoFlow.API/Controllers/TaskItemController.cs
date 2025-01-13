using Microsoft.AspNetCore.Mvc;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemService _taskItemService;

        public TaskItemController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

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

        [HttpGet("Id")]
        public async Task<ActionResult> ReadTaskItemAsync(int id)
        {
            return Ok(await _taskItemService.ReadTaskItemAsync(id));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTaskItemAsync(TaskItemUpdateDto taskItemUpdateDto)
        {
            return Ok(await _taskItemService.UpdateTaskItemAsync(taskItemUpdateDto));
        }

        [HttpDelete("Id")]
        public async Task<ActionResult> DeleteTaskItemAsync(int id)
        {
            return Ok(await _taskItemService.DeleteTaskItemAsync(id));
        }
    }
}
