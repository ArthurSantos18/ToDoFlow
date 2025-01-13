using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
using ToDoFlow.Services.Services;

namespace ToDoFlow.Infrastructure.Repositories.Interface
{
    public interface IAccountService
    {
        public Task<ApiResponse<string>> LoginAsync(LoginRequestDto loginRequestDto);
        public Task<ApiResponse<string>> RegisterAsync(RegisterRequestDto registerRequestDto);
        public string GenerateToken(User user);

    }
}
