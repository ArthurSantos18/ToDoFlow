using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
using ToDoFlow.Services.Services.Utils;

namespace ToDoFlow.Services.Services.Interface
{
    public interface IAccountService
    {
        public Task<ApiResponse<string, UserRefreshTokenReadDto>> LoginAsync(LoginRequestDto loginRequestDto);
        public Task<ApiResponse<string, UserRefreshTokenReadDto>> RegisterAsync(RegisterRequestDto registerRequestDto);
        public Task<ApiResponse<string, UserRefreshTokenReadDto>> RefreshToken(UserRefreshTokenRefreshDto refreshToken);
        public Task<ApiResponse> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
        public Task<ApiResponse> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        

    }
}
