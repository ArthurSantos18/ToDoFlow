using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Repositories.Interface
{
    public interface IUserRepository
    {
        public Task<List<User>> CreateUserAsync(User user);
        public Task<List<User>> ReadUserAsync();
        public Task<User> ReadUserAsync(int id);
        public Task<User> ReadUserAsync(string email);
        public Task<List<User>> UpdateUserAsync(User user);
        public Task<List<User>> DeleteUserAsync(int id);
    }
}
