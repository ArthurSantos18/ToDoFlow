using AutoMapper;
using ToDoFlow.Infrastructure.Repositories.Interface;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IEncryptionService _encryptionService;

        public UserService(IUserRepository userRepository, IMapper mapper, IEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _encryptionService = encryptionService;
        }

        public async Task<ApiResponse<List<UserReadDto>>> CreateUserAsync(UserCreateDto userCreateDto)
        {
            try
            {
                User user = _mapper.Map<User>(userCreateDto);
                user.PasswordHash = _encryptionService.HashPassword(user.PasswordHash);
                await _userRepository.CreateUserAsync(user);

                List<User> users = await _userRepository.ReadUserAsync();
                List<UserReadDto> userReadDtos = _mapper.Map<List<UserReadDto>>(users);

                return new ApiResponse<List<UserReadDto>>(userReadDtos, true, "Usuário criado com sucesso", 201);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserReadDto>>(null, false, $"Erro: ${ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<UserReadDto>>> ReadUserAsync()
        {
            try
            {
                List<User> users = await _userRepository.ReadUserAsync();
                List<UserReadDto> userReadDtos = _mapper.Map<List<UserReadDto>>(users);

                return new ApiResponse<List<UserReadDto>>(userReadDtos, true, "Operação realizada com sucesso", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<UserReadDto>> ReadUserAsync(int id)
        {
            try
            {
                User user = await _userRepository.ReadUserAsync(id);
                UserReadDto userReadDto = _mapper.Map<UserReadDto>(user);

                return new ApiResponse<UserReadDto>(userReadDto, true, "Operação realizada com sucesso", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserReadDto>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<UserReadDto>>> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            try
            {
                User user = await _userRepository.ReadUserAsync(userUpdateDto.Id);
                _mapper.Map(userUpdateDto, user);
                user.PasswordHash = _encryptionService.HashPassword(userUpdateDto.PasswordHash);
                await _userRepository.UpdateUserAsync(user);

                List<User> users = await _userRepository.ReadUserAsync();
                List<UserReadDto> userReadDtos = _mapper.Map<List<UserReadDto>>(users);

                return new ApiResponse<List<UserReadDto>>(userReadDtos, true, "Usuário editado com sucesso", 200);

            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<UserReadDto>>> DeleteUserAsync(int id)
        {
            try
            {
                await _userRepository.DeleteUserAsync(id);

                List<User> users = await _userRepository.ReadUserAsync();
                List<UserReadDto> userReadDtos = _mapper.Map<List<UserReadDto>>(users);

                return new ApiResponse<List<UserReadDto>>(userReadDtos, true, "Usuário deletado com sucesso", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }
    }
}
