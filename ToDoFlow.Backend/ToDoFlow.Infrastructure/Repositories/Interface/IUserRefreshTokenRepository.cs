using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Repositories.Interface
{
    public interface IUserRefreshTokenRepository
    {
        Task<UserRefreshToken?> ReadUserRefreshByTokenAsync(string refreshToken);
        Task<UserRefreshToken?> ReadUserRefreshByUserIdAsync(int userId);
        Task CreateUserRefreshTokenAsync(UserRefreshToken userRefreshToken);
        Task DeleteUserRefreshTokenAsync(string refreshToken);
        Task DeleteExpiredTokensByUserIdAsync(int userId);
    }
}
