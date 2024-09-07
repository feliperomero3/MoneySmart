using System.Threading.Tasks;
using MoneySmart.Services;

namespace MoneySmart.IntegrationTests.Identity;

public class TestEmailSender : IEmailSender
{
    public Task SendEmailAsync(string inputEmail, string subject, string htmlMessage)
    {
        return Task.CompletedTask;
    }
}
