using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;

namespace ToDoFlow.Services.Services.Interface
{
    public interface IAccountService
    {
        public Task<ApiResponse<string, UserRefreshTokenReadDto>> LoginAsync(LoginRequestDto loginRequestDto);
        public Task<ApiResponse<string, UserRefreshTokenReadDto>> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<ApiResponse<string, UserRefreshTokenReadDto>> RefreshToken(UserRefreshTokenRefreshDto refreshToken);
        public string GenerateToken(User user, string purpose, int expiryMinutes);
        public UserRefreshToken GenerateRefreshToken(User user);

    }
}
