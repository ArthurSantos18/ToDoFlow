using ToDoFlow.Domain.Models;

namespace ToDoFlow.Services.Services.Interface
{
    public interface ITokenService
    {
        public string GenerateToken(User user, string purpose, int expiryMinutes);
        public UserRefreshToken GenerateRefreshToken(User user);
        public bool ValidateToken(string token);
    }
}
