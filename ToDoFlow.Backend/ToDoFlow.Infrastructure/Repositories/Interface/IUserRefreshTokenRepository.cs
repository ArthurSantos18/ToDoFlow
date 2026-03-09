using ToDoFlow.Domain.Models;

namespace ToDoFlow.Infrastructure.Repositories.Interface
{
    public interface IUserRefreshTokenRepository
    {
        Task<UserRefreshToken?> GetUserRefreshByTokenAsync(string refreshToken);
        Task<UserRefreshToken?> GetUserRefreshByUserIdAsync(int userId);
        Task CreateUserRefreshTokenAsync(UserRefreshToken userRefreshToken);
        Task DeleteUserRefreshTokenAsync(string refreshToken);
        Task DeleteExpiredTokensByUserIdAsync(int userId);
    }
}
