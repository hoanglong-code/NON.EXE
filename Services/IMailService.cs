using NON.EXE.Models;

namespace NON.EXE.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
