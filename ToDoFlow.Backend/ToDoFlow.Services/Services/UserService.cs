using AutoMapper;
using ToDoFlow.Infrastructure.Repositories.Interface;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
using ToDoFlow.Services.Services.Interface;
using ToDoFlow.Services.Services.Utils;

namespace ToDoFlow.Services.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper, IPasswordService passwordService, 
        ICategoryRepository categoryRepository, ITaskItemRepository taskItemRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly ITaskItemRepository _taskItemRepository = taskItemRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IPasswordService _passwordService = passwordService;

        public async Task<ApiResponse<UserReadDto>> CreateUserAsync(UserCreateDto userCreateDto)
        {
            try
            {
                User user = _mapper.Map<User>(userCreateDto);
                user.Password = _passwordService.HashPassword(user.Password);
                await _userRepository.CreateUserAsync(user);

                UserReadDto userReadDto = _mapper.Map<UserReadDto>(user);

                return new ApiResponse<UserReadDto>(userReadDto, true, "User created successfully", 201);
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserReadDto>(null!, false, $"Erro: ${ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<IEnumerable<UserReadDto>>> GetUserAsync()
        {
            try
            {
                IEnumerable<User> users = await _userRepository.GetUserAsync();
                IEnumerable<UserReadDto> userReadDtos = _mapper.Map<IEnumerable<UserReadDto>>(users);
                
                foreach (UserReadDto user in userReadDtos)
                {
                    IEnumerable<Category> categories = await _categoryRepository.GetCategoryByUserAsync(user.Id);
                    user.CategoryCount = categories.Count();

                    IEnumerable<TaskItem> taskItems = await _taskItemRepository.GetTaskItemByUserAsync(user.Id);
                    user.TaskItemCount = taskItems.Count();
                }

                return new ApiResponse<IEnumerable<UserReadDto>>(userReadDtos, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<IEnumerable<UserReadDto>>(null!, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<UserReadDto>> GetUserByIdAsync(int id)
        {
            try
            {
                User user = await _userRepository.GetUserByIdAsync(id);

                if (user == null)
                {
                    return new ApiResponse<UserReadDto>(null, false, "User not found", 404);
                }

                UserReadDto userReadDto = _mapper.Map<UserReadDto>(user);

                return new ApiResponse<UserReadDto>(userReadDto, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserReadDto>(null!, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<UserReadDto>> GetUserByEmailAsync(string email)
        {
            try
            {
                User user = await _userRepository.GetUserByEmailAsync(email);

                if (user == null)
                {
                    return new ApiResponse<UserReadDto>(null, false, "User not found", 404);
                }

                UserReadDto userReadDto = _mapper.Map<UserReadDto>(user);

                return new ApiResponse<UserReadDto>(userReadDto, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserReadDto>(null!, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<UserReadDto>> UpdateUserAsync(int id, UserUpdateDto userUpdateDto)
        {
            try
            {
                User user = await _userRepository.GetUserByIdAsync(id);

                if (userUpdateDto.Password != null)
                {
                    userUpdateDto.Password = _passwordService.HashPassword(userUpdateDto.Password);
                }
                else
                {
                    userUpdateDto.Password = user.Password;
                    userUpdateDto.ConfirmPassword = user.Password;
                }

                _mapper.Map(userUpdateDto, user);

                await _userRepository.UpdateUserAsync(user);

                
                UserReadDto userReadDto = _mapper.Map<UserReadDto>(user);

                return new ApiResponse<UserReadDto>(userReadDto, true, "User edited successfully", 200);

            }
            catch (Exception ex)
            {
                return new ApiResponse<UserReadDto>(null!, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse> DeleteUserAsync(int id)
        {
            try
            {
                await _userRepository.DeleteUserAsync(id);

                return new ApiResponse(true, "User deleted successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, $"Erro: {ex.Message}", 500);
            }
        }
    }
}
