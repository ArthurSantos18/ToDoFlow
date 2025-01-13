using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Infrastructure.Repositories.Interface;

namespace ToDoFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var token = await _accountService.LoginAsync(loginRequestDto);

            if (token == null)
            {
                return Unauthorized(token);
            }

            return Ok(token);
        }
    }
}
