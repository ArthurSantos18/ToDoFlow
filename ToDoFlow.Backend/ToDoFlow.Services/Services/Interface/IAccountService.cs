using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;

namespace ToDoFlow.Services.Services.Interface
{
    public interface IAccountService
    {
        public Task<ApiResponse<string>> LoginAsync(LoginRequestDto loginRequestDto);
        public Task<ApiResponse<string>> RegisterAsync(RegisterRequestDto registerRequestDto);
        public string GenerateToken(User user);

    }
}
