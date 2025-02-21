using Microsoft.EntityFrameworkCore;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Context;
using ToDoFlow.Infrastructure.Repositories.Interface;

namespace ToDoFlow.Infrastructure.Repositories
{
    public class UserRefreshTokenRepository(ToDoFlowContext context) : IUserRefreshTokenRepository
    {
        private readonly ToDoFlowContext _context = context;

        public async Task CreateUserRefreshTokenAsync(UserRefreshToken userRefreshToken)
        {
            await _context.UserRefreshToken.AddAsync(userRefreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task<UserRefreshToken?> ReadUserRefreshByTokenAsync(string refreshToken)
        {
            return await _context.UserRefreshToken.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<UserRefreshToken?> ReadUserRefreshByUserIdAsync(int userId)
        {
            return await _context.UserRefreshToken.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task DeleteUserRefreshTokenAsync(string refreshToken)
        {
            UserRefreshToken? userRefreshToken = await _context.UserRefreshToken.Where(u => u.RefreshToken == refreshToken).FirstOrDefaultAsync();
            
            _context.UserRefreshToken.Remove(userRefreshToken);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExpiredTokensByUserIdAsync(int userId)
        {
            List<UserRefreshToken> expiredTokens = await _context.UserRefreshToken.Where(t => t.Expiration < DateTime.UtcNow && t.UserId == userId).ToListAsync();

            if (expiredTokens.Count != 0)
            {
                _context.UserRefreshToken.RemoveRange(expiredTokens);
                await _context.SaveChangesAsync();
            }
        }
    }
}
