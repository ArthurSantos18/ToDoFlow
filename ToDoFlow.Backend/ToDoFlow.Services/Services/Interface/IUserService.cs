using ToDoFlow.Application.Dtos;

namespace ToDoFlow.Services.Services.Interface
{
    public interface IUserService
    {
        public Task<ApiResponse<List<UserReadDto>>> CreateUserAsync(UserCreateDto userCreateDto);
        public Task<ApiResponse<List<UserReadDto>>> ReadUserAsync();
        public Task<ApiResponse<UserReadDto>> ReadUserByIdAsync(int id);
        public Task<ApiResponse<UserReadDto>> ReadUserByEmailAsync(string email);
        public Task<ApiResponse<List<UserReadDto>>> UpdateUserAsync(int id, UserUpdateDto userUpdateDto);
        public Task<ApiResponse<List<UserReadDto>>> DeleteUserAsync(int id);
    }
}
