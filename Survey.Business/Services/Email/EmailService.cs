// Hint: will need download MailKit Package...

namespace Survey.Business.Services.Email
{
    public class EmailService : IEmailSender // this IEmailSender is exist in .NET...
    {
        private readonly MailSettings _settings;

        public EmailService(IOptions<MailSettings> settings) // don't forget register it in DI --> services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));
        {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_settings.Mail),
                Subject = subject,
            };

            message.To.Add(InternetAddress.Parse(email));

            var builder = new BodyBuilder()
            {
                HtmlBody = htmlMessage
            };

            message.Body = builder.ToMessageBody();

            var smtp = new SmtpClient(); // Note, SmtpClient package that exist in MailKit not System...
            smtp.Connect(_settings.Host, _settings.Port,SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_settings.Mail, _settings.Password);
            smtp.Send(message);
            smtp.Disconnect(true);
        }
    }
}


