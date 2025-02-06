using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.API.Controllers
{
    [Route("api/enums")]
    [ApiController]
    public class EnumController(IEnumService enumService) : ControllerBase
    {
        private readonly IEnumService enumService = enumService;

        [HttpGet("priorities")]
        public IActionResult GetPriorities()
        {
            return Ok(enumService.ReadPriorities());
        }
    }
}
