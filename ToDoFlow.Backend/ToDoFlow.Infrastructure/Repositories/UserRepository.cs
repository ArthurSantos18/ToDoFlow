using Microsoft.EntityFrameworkCore;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Context;
using ToDoFlow.Infrastructure.Repositories.Interface;

namespace ToDoFlow.Infrastructure.Repositories
{
    public class UserRepository(ToDoFlowContext context) : IUserRepository
    {
        private readonly ToDoFlowContext _context = context;

        public async Task<List<User>> CreateUserAsync(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return await _context.Users.ToListAsync();
        }

        public async Task<List<User>> ReadUserAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> ReadUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> ReadUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<User>> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return await _context.Users.ToListAsync();
        }
        public async Task<List<User>> DeleteUserAsync(int id)
        {
            _context.Users.Remove(await _context.Users.FirstOrDefaultAsync(u => u.Id == id));
            await _context.SaveChangesAsync();

            return await _context.Users.ToListAsync();
        }
    }
}
