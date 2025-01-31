using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Services.Services;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AccountController(IAccountService accountService) : ControllerBase
    {
        private readonly IAccountService _accountService = accountService;

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginRequestDto loginRequestDto)
        {
            ApiResponse<string> token = await _accountService.LoginAsync(loginRequestDto);

            if (token == null)
            {
                return Unauthorized(token);
            }

            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            ApiResponse<string> token = await _accountService.RegisterAsync(registerRequestDto);
            
            if (token == null)
            {
                return BadRequest(token);
            }

            return Ok(token);
        }
    }
}
