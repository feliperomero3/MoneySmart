using System.Threading.Tasks;

namespace MoneySmart.Services;

public interface IEmailSender
{
    Task SendEmailAsync(string inputEmail, string subject, string htmlMessage);
}

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string inputEmail, string subject, string htmlMessage)
    {
        return Task.CompletedTask;
    }
}
