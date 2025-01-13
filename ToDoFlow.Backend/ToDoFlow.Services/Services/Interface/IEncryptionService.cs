namespace ToDoFlow.Services.Services.Interface
{
    public interface IEncryptionService
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hash);
    }
}
