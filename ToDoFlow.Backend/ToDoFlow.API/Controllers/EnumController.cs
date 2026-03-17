using Microsoft.AspNetCore.Mvc;
using ToDoFlow.Application.Services.Interfaces;
using ToDoFlow.Application.Services.Utils;

namespace ToDoFlow.API.Controllers
{
    [Route("api/enums")]
    [ApiController]
    public class EnumController(IEnumService enumService) : ControllerBase
    {
        private readonly IEnumService _enumService = enumService;

        [HttpGet("priorities")]
        public async Task<ActionResult<ApiResponse<Dictionary<int, string>>>> GetPriorities()
        {
            var response = _enumService.GetPriorities();
            
            return StatusCode(response.HttpStatus, response);
        }
    }
}
