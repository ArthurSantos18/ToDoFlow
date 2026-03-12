using ToDoFlow.Application.Dtos;
using ToDoFlow.Application.Services.Utils;
using ToDoFlow.Domain.Models;

namespace ToDoFlow.Application.Services.Interface
{
    public interface IAccountService
    {
        public Task<ApiResponse<string, UserRefreshTokenReadDto>> LoginAsync(LoginRequestDto loginRequestDto);
        public Task<ApiResponse<string, UserRefreshTokenReadDto>> RegisterAsync(RegisterRequestDto registerRequestDto);
        public Task<ApiResponse<string, UserRefreshTokenReadDto>> RefreshTokenAsync(UserRefreshTokenRefreshDto refreshToken);
        public Task<ApiResponse> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        public Task<ApiResponse> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        

    }
}
