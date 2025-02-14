namespace ToDoFlow.Domain.Models
{
    public class UserRefreshToken
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
