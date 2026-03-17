using System.Security.Claims;
using ToDoFlow.Domain.Models;

namespace ToDoFlow.Application.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user, string purpose);
        public UserRefreshToken GenerateRefreshToken(User user);
        public ClaimsPrincipal ValidateToken(string token);
    }
}
