using ToDoFlow.Application.Dtos;
using ToDoFlow.Application.Services.Utils;

namespace ToDoFlow.Application.Services.Interface
{
    public interface IUserService
    {
        public Task<ApiResponse<UserReadDto>> CreateUserAsync(UserCreateDto userCreateDto);
        public Task<ApiResponse<IEnumerable<UserReadDto>>> GetUserAsync();
        public Task<ApiResponse<UserReadDto>> GetUserByIdAsync(int id);
        public Task<ApiResponse<UserReadDto>> GetUserByEmailAsync(string email);
        public Task<ApiResponse<UserReadDto>> UpdateUserAsync(int id, UserUpdateDto userUpdateDto);
        public Task<ApiResponse> DeleteUserAsync(int id);
    }
}
