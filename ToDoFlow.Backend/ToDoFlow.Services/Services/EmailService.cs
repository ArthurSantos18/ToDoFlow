using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.Services.Services
{
    public class EmailService(IConfiguration configuration) : IEmailService
    {
        private readonly IConfiguration _configuration = configuration;

        public async Task<bool> SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                MailMessage mailMessage = new()
                {
                    From = new MailAddress(
                        _configuration["EmailSettings:SmtpFrom"], 
                        _configuration["EmailSettings:SmtpName"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(new MailAddress(email));
                mailMessage.Priority = MailPriority.High;

                using SmtpClient smtp = new SmtpClient(_configuration["EmailSettings:SmtpServer"]);

                smtp.Port = int.Parse(_configuration["EmailSettings:SmtpPort"]);
                smtp.Credentials = new NetworkCredential(
                    _configuration["EmailSettings:SmtpFrom"],
                    _configuration["EmailSettings:SmtpPassword"]
                    );
                smtp.EnableSsl = true;

                await smtp.SendMailAsync(mailMessage);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
