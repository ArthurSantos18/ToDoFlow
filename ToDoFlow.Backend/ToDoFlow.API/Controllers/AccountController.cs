using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
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
            ApiResponse<string, UserRefreshTokenDto> response = await _accountService.LoginAsync(loginRequestDto);
            
            if (response == null)
            {
                return Unauthorized(response);
            }
            
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            ApiResponse<string, UserRefreshTokenDto> response = await _accountService.RegisterAsync(registerRequestDto);
            
            if (response.Data1 == null && response.Data2 == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshTokenAsync(string refreshToken)
        {
            ApiResponse<string, UserRefreshTokenDto> response = await _accountService.RefreshToken(refreshToken);
            
            if (response.Data1 == null && response.Data2 == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
