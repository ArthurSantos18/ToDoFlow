using Microsoft.EntityFrameworkCore;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Context;
using ToDoFlow.Infrastructure.Repositories.Interface;

namespace ToDoFlow.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ToDoFlowContext _context;

        public UserRepository(ToDoFlowContext context)
        {
            _context = context;
        }

        public async Task<List<User>> CreateUserAsync(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return await _context.Users.Include(c => c.Categories).ThenInclude(t => t.Tasks).ToListAsync();
        }

        public async Task<List<User>> ReadUserAsync()
        {
            return await _context.Users.Include(c => c.Categories).ThenInclude(t => t.Tasks).ToListAsync();
        }

        public async Task<User> ReadUserAsync(int id)
        {
            return await _context.Users.Include(c => c.Categories).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> UpdateUserAsync(User user)
        {

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return await _context.Users.Include(c => c.Categories).ThenInclude(t => t.Tasks).ToListAsync();

        }
        public async Task<List<User>> DeleteUserAsync(int id)
        {
            User user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return await _context.Users.Include(c => c.Categories).ThenInclude(t => t.Tasks).ToListAsync();
        }
    }
}
