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
        public async Task<ActionResult> LoginAsync(LoginRequestDto loginRequestDto)
        {
            ApiResponse<string, UserRefreshTokenReadDto> response = await _accountService.LoginAsync(loginRequestDto);
            
            if (response == null)
            {
                return Unauthorized(response);
            }
            
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            ApiResponse<string, UserRefreshTokenReadDto> response = await _accountService.RegisterAsync(registerRequestDto);
            
            if (response.Data1 == null && response.Data2 == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpPost("refresh")]
        public async Task<ActionResult> RefreshTokenAsync(UserRefreshTokenRefreshDto userRefreshTokenRefreshDto)
        {
            ApiResponse<string, UserRefreshTokenReadDto> response = await _accountService.RefreshToken(userRefreshTokenRefreshDto);

           
            if (response.Data1 == null && response.Data2 == null)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            ApiResponse response = await _accountService.ForgotPasswordAsync(forgotPasswordDto);
            
            if (response.Success == false)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            ApiResponse response = await _accountService.ResetPasswordAsync(resetPasswordDto);
            return Ok(response);
        }
    }
}
