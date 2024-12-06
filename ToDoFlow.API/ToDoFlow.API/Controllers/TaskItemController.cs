using Microsoft.AspNetCore.Mvc;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Infrastructure.Repositories.Interface;

namespace ToDoFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public TaskItemController(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateTaskItemAsync(TaskItemCreateDto taskItemCreateDto)
        {
            return Ok(await _taskItemRepository.CreateTaskItemAsync(taskItemCreateDto));
        }

        [HttpGet]
        public async Task<ActionResult> ReadTaskItemAsync()
        {
            return Ok(await _taskItemRepository.ReadTaskItemAsync());
        }

        [HttpGet("Id")]
        public async Task<ActionResult> ReadTaskItemAsync(int id)
        {
            return Ok(await _taskItemRepository.ReadTaskItemAsync(id));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTaskItemAsync(TaskItemUpdateDto taskItemUpdateDto)
        {
            return Ok(await _taskItemRepository.UpdateTaskItemAsync(taskItemUpdateDto));
        }

        [HttpDelete("Id")]
        public async Task<ActionResult> DeleteTaskItemAsync(int id)
        {
            return Ok(await _taskItemRepository.DeleteTaskItemAsync(id));
        }
    }
}
