using System.Net.Mail;
using EmailSender.Messages;
using Rebus.Handlers;

namespace EmailSender.Handlers
{
    internal class SendEmailHandler : IHandleMessages<SendEmail>
    {
        private readonly SmtpClient _smtpClient;

        public SendEmailHandler(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public async Task Handle(SendEmail message)
        {
            Console.WriteLine($"Sending email to {message.Recipient}");

            await _smtpClient.SendMailAsync(new MailMessage
            {
                To = { message.Recipient },
                Subject = message.Subject,
                Body = message.Body,
                From = new MailAddress("info@rebus.fm")
            });
        }
    }
}