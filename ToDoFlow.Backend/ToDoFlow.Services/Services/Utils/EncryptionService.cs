using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.Services.Services.Utils
{
    public class EncryptionService : IEncryptionService
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
