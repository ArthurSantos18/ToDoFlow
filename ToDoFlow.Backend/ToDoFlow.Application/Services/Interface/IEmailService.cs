using Microsoft.Extensions.Configuration;

namespace ToDoFlow.Application.Services.Interface
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(string email, string subject, string message);
    }
}
