namespace ToDoFlow.Application.Dtos
{
    public class UserRefreshTokenBaseDto
    {
        public required string RefreshToken { get; set; }
    }

    public class UserRefreshTokenReadDto : UserRefreshTokenBaseDto
    {
        public DateTime Expiration { get; set; }
        public int UserId { get; set; }
    }

    public class UserRefreshTokenRefreshDto : UserRefreshTokenBaseDto { }
}
