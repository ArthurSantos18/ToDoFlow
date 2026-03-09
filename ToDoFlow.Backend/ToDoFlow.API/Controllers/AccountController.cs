using Microsoft.AspNetCore.Mvc;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Services.Services.Interface;
using ToDoFlow.Services.Services.Utils;

namespace ToDoFlow.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AccountController(IAccountService accountService) : ControllerBase
    {
        private readonly IAccountService _accountService = accountService;

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<string, UserRefreshTokenReadDto>>> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var response = await _accountService.LoginAsync(loginRequestDto);
            
            return StatusCode(response.HttpStatus, response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<string, UserRefreshTokenReadDto>>> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            var response = await _accountService.RegisterAsync(registerRequestDto);

            return StatusCode(response.HttpStatus, response);
        }
        [HttpPost("refresh")]
        public async Task<ActionResult<ApiResponse<string, UserRefreshTokenReadDto>>> RefreshTokenAsync(UserRefreshTokenRefreshDto userRefreshTokenRefreshDto)
        {
            var response = await _accountService.RefreshTokenAsync(userRefreshTokenRefreshDto);

            return StatusCode(response.HttpStatus, response);
        }
        [HttpPost("forgot-password")]
        public async Task<ActionResult<ApiResponse>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            var response = await _accountService.ForgotPasswordAsync(forgotPasswordDto);

            return StatusCode(response.HttpStatus, response);
        }
        [HttpPost("reset-password")]
        public async Task<ActionResult<ApiResponse>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            var response = await _accountService.ResetPasswordAsync(resetPasswordDto);
            
            return StatusCode(response.HttpStatus, response);
        }
    }
}
