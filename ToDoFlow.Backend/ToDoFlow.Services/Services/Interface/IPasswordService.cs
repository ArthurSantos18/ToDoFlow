namespace ToDoFlow.Services.Services.Interface
{
    public interface IPasswordService
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hash);
    }
}
