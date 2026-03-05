using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Repositories.Interface
{
    public interface IUserRepository
    {
        public Task<User> CreateUserAsync(User user);
        public Task<List<User>> ReadUserAsync();
        public Task<User> ReadUserByIdAsync(int id);
        public Task<User> ReadUserByEmailAsync(string email);
        public Task<User> UpdateUserAsync(User user);
        public Task<bool> DeleteUserAsync(int id);
    }
}
