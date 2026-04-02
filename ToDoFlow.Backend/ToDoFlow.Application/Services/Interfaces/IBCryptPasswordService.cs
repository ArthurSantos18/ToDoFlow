namespace ToDoFlow.Application.Services.Interfaces
{
    public interface IBCryptPasswordService
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hash);
    }
}
