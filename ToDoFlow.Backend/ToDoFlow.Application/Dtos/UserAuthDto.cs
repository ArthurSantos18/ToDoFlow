using ToDoFlow.Domain.Models.Enums;

namespace ToDoFlow.Application.Dtos
{
    public abstract class UserAuthDtoBase
    {
        public required string Email { get; set; }   
        public required string Password { get; set; }
    }
    public class LoginRequestDto : UserAuthDtoBase { }

    public class RegisterRequestDto : UserAuthDtoBase
    {
        public required string Name { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}
