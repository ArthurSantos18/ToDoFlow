using ToDoFlow.Application.Dtos;

namespace ToDoFlow.Services.Services.Interface
{
    public interface IUserService
    {
        public Task<ApiResponse<List<UserReadDto>>> CreateUserAsync(UserCreateDto userCreateDto);
        public Task<ApiResponse<List<UserReadDto>>> ReadUserAsync();
        public Task<ApiResponse<UserReadDto>> ReadUserAsync(int id);
        public Task<ApiResponse<List<UserReadDto>>> UpdateUserAsync(UserUpdateDto userUpdateDto);
        public Task<ApiResponse<List<UserReadDto>>> DeleteUserAsync(int id);
    }
}
