using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Context;
using ToDoFlow.Infrastructure.Repositories.Interface;
using ToDoFlow.Services.Services;

namespace ToDoFlow.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ToDoFlowContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ToDoFlowContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<UserReadDto>>> CreateUserAsync(UserCreateDto UserCreateDto)
        {
            try
            {
                User user = _mapper.Map<User>(UserCreateDto);
                await _context.AddAsync(user);
                await _context.SaveChangesAsync();

                List<User> users = await _context.Users.Include(c => c.Categories).ThenInclude(t => t.Tasks).ToListAsync();
                List<UserReadDto> userReadDtos = _mapper.Map<List<UserReadDto>>(users);

                return new ApiResponse<List<UserReadDto>>(userReadDtos, true, "Usuário adicionado com sucesso", 201);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<List<UserReadDto>>> ReadUserAsync()
        {
            try
            {
                List<User> users = await _context.Users.Include(c => c.Categories).ThenInclude(t => t.Tasks).ToListAsync();
                List<UserReadDto> userReadDtos = _mapper.Map<List<UserReadDto>>(users);

                return new ApiResponse<List<UserReadDto>>(userReadDtos, true, "Operação realizada com sucesso", 201);
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
                User user = await _context.Users.Include(c => c.Categories).FirstOrDefaultAsync(u => u.Id == id);
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
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userUpdateDto.Id);
                _mapper.Map(userUpdateDto, user);
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                List<User> users = await _context.Users.Include(c => c.Categories).ThenInclude(t => t.Tasks).ToListAsync();
                List<UserReadDto> userReadDtos = _mapper.Map<List<UserReadDto>>(users);

                return new ApiResponse<List<UserReadDto>>(userReadDtos, false, "Usuário atualizado com sucesso", 204);
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
                User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                List<User> users = await _context.Users.Include(c => c.Categories).ThenInclude(t => t.Tasks).ToListAsync();
                List<UserReadDto> userReadDtos = _mapper.Map<List<UserReadDto>>(users);

                return new ApiResponse<List<UserReadDto>>(userReadDtos, true, "Usuário deletado com sucesso", 204);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserReadDto>>(null, false, $"Erro: {ex.Message}", 500);
            }
        }
    }
}
