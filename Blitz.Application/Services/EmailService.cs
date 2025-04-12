using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using Blitz.Application.Interfaces;
using Blitz.Application.Dtos;

namespace Blitz.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task SendEmailAsync(string body, string subject, string toAddress, AttachmentPayload attachment)
        {
            var to = new MailAddress(toAddress);
            var from = new MailAddress(_config["Email:From"]);

            var email = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body,
            };

            email.Attachments.Add(new Attachment(attachment.Content, attachment.FileName));

            var smtp = new SmtpClient
            {
                Host = _config["SMTP:Host"],
                Port = int.Parse(_config["SMTP:Port"]),
                Credentials = new NetworkCredential(_config["Email:From"], _config["SMTP:NetworkPassword"]),
                EnableSsl = true
            };

            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

            try
            {
                await smtp.SendMailAsync(email);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task SendEmailsAsync(EmailPayload payload)
        {
            try
            {
                var tasksList = new List<Task>();
                var memoryStream = new MemoryStream();
                await payload.Attachment.CopyToAsync(memoryStream);

                memoryStream.Seek(0, SeekOrigin.Begin);

                var attachmentPayload = new AttachmentPayload
                {
                    Content = memoryStream,
                    FileName = payload.Attachment.FileName
                };

                foreach (var address in payload.EmailAddresses)
                {
                    tasksList.Add(SendEmailAsync(payload.Body, payload.Subject, address, attachmentPayload));
                }

                await Task.WhenAll(tasksList);
            }
            catch (SmtpException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}