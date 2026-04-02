using ToDoFlow.Application.Services.Interfaces;

namespace ToDoFlow.Application.Services.Utils
{
    public class BCryptPasswordService : IBCryptPasswordService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
