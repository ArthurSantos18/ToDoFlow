namespace ToDoFlow.Application.Dtos
{
    public class ForgotPasswordDtoBase
    {
        public required string Email { get; set; }
    }

    public class ForgotPasswordDto : ForgotPasswordDtoBase { }

    public class ResetPasswordDto : ForgotPasswordDtoBase
    {
        public required string Token { get; set; }
        public required string NewPassword { get; set; }
        public required string ConfirmPassword { get; set; }

    }
}
