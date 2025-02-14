using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;

namespace ToDoFlow.Services.Services.Interface
{
    public interface IAccountService
    {
        public Task<ApiResponse<string, UserRefreshTokenDto>> LoginAsync(LoginRequestDto loginRequestDto);
        public Task<ApiResponse<string, UserRefreshTokenDto>> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<ApiResponse<string, UserRefreshTokenDto>> RefreshToken(string refreshToken);
        public string GenerateToken(User user, string purpose, int expiryMinutes);
        public UserRefreshToken GenerateRefreshToken(User user);

    }
}
