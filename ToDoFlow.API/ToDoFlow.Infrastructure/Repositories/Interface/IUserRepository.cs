using ToDoFlow.Application.Dtos;
using ToDoFlow.Services.Services;

namespace ToDoFlow.Infrastructure.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<ApiResponse<List<UserReadDto>>> CreateUserAsync(UserCreateDto UserCreateDto);
        Task<ApiResponse<List<UserReadDto>>> ReadUserAsync();
        Task<ApiResponse<UserReadDto>> ReadUserAsync(int id);
        Task<ApiResponse<List<UserReadDto>>> UpdateUserAsync(UserUpdateDto UserUpdateDto);
        Task<ApiResponse<List<UserReadDto>>> DeleteUserAsync(int id);
    }
}
