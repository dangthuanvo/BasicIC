using System.Threading.Tasks;

namespace BasicIC.Services.Interfaces
{
    public interface ISendEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}