using Blitz.Application.Dtos;

namespace Blitz.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string body, string subject, string toAddress, AttachmentPayload attachment);
        Task SendEmailsAsync(EmailPayload payload);
    }
}
