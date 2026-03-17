using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> CreateUserAsync(User user);
        public Task<IEnumerable<User>> GetUserAsync();
        public Task<User> GetUserByIdAsync(int id);
        public Task<User> GetUserByEmailAsync(string email);
        public Task<User> UpdateUserAsync(User user);
        public Task<bool> DeleteUserAsync(int id);
    }
}
