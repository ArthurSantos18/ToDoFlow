using AutoMapper;
using ToDoFlow.Infrastructure.Repositories.Interface;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
using ToDoFlow.Services.Services.Interface;
using ToDoFlow.Infrastructure.Repositories;

namespace ToDoFlow.Services.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper, IEncryptionService encryptionService, 
        ICategoryRepository categoryRepository, ITaskItemRepository taskItemRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly ITaskItemRepository _taskItemRepository = taskItemRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IEncryptionService _encryptionService = encryptionService;

        public async Task<ApiResponse<List<UserReadDto>>> CreateUserAsync(UserCreateDto userCreateDto)
        {
            try
            {
                User user = _mapper.Map<User>(userCreateDto);
                user.Password = _encryptionService.HashPassword(user.Password);
                await _userRepository.CreateUserAsync(user);

                List<User> users = await _userRepository.ReadUserAsync();
                List<UserReadDto> userReadDtos = _mapper.Map<List<UserReadDto>>(users);

                return new ApiResponse<List<UserReadDto>>(userReadDtos, true, "User created successfully", 201);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserReadDto>>(null!, false, $"Erro: ${ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<UserReadDto>>> ReadUserAsync()
        {
            try
            {
                List<User> users = await _userRepository.ReadUserAsync();
                List<UserReadDto> userReadDtos = _mapper.Map<List<UserReadDto>>(users);
                
                foreach (UserReadDto user in userReadDtos)
                {
                    List<Category> categories = await _categoryRepository.ReadCategoryByUserAsync(user.Id);
                    user.CategoryCount = categories.Count;

                    List<TaskItem> taskItems = await _taskItemRepository.ReadTaskItemByUserAsync(user.Id);
                    user.TaskItemCount = taskItems.Count;
                }

                return new ApiResponse<List<UserReadDto>>(userReadDtos, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserReadDto>>(null!, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<UserReadDto>> ReadUserByIdAsync(int id)
        {
            try
            {
                User user = await _userRepository.ReadUserByIdAsync(id);
                UserReadDto userReadDto = _mapper.Map<UserReadDto>(user);

                return new ApiResponse<UserReadDto>(userReadDto, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserReadDto>(null!, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<UserReadDto>>> UpdateUserAsync(int id, UserUpdateDto userUpdateDto)
        {
            try
            {
                User user = await _userRepository.ReadUserByIdAsync(id);
                _mapper.Map(userUpdateDto, user);
                user.Password = _encryptionService.HashPassword(userUpdateDto.Password);
                await _userRepository.UpdateUserAsync(user);

                List<User> users = await _userRepository.ReadUserAsync();
                List<UserReadDto> userReadDtos = _mapper.Map<List<UserReadDto>>(users);

                return new ApiResponse<List<UserReadDto>>(userReadDtos, true, "User edited successfully", 200);

            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserReadDto>>(null!, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<UserReadDto>>> DeleteUserAsync(int id)
        {
            try
            {
                await _userRepository.DeleteUserAsync(id);

                List<User> users = await _userRepository.ReadUserAsync();
                List<UserReadDto> userReadDtos = _mapper.Map<List<UserReadDto>>(users);

                return new ApiResponse<List<UserReadDto>>(userReadDtos, true, "User deleted successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserReadDto>>(null!, false, $"Erro: {ex.Message}", 500);
            }
        }
    }
}
