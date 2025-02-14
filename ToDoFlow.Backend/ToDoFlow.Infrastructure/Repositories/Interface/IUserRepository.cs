using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Repositories.Interface
{
    public interface IUserRepository
    {
        public Task<List<User>> CreateUserAsync(User user);
        public Task<List<User>> ReadUserAsync();
        public Task<User> ReadUserByIdAsync(int id);
        public Task<User> ReadUserByEmailAsync(string email);
        public Task<List<User>> UpdateUserAsync(User user);
        public Task<List<User>> DeleteUserAsync(int id);
    }
}
