using Microsoft.AspNetCore.Mvc;
using ToDoFlow.Services.Services.Interface;
using ToDoFlow.Services.Services.Utils;

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
            var response = _enumService.ReadPriorities();
            
            return StatusCode(response.HttpStatus, response);
        }
    }
}
