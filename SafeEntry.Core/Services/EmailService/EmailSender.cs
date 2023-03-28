using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using Org.BouncyCastle.Asn1.Ocsp;
using SafeEntry.Core.Interfaces;
using SafeEntry.Core.Utils;
using static Org.BouncyCastle.Math.EC.ECCurve;


namespace SafeEntry.Core.Services.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }
        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("SafeEntry Support", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart("html") { Text = message.Content };
            return emailMessage;
        }
        private void Send(MimeMessage mailMessage)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    try
                    {
                        client.Authenticate(_emailConfig.From, "tvqkswvspilimcyv");
                        client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, false);
                        client.Send(mailMessage);
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        client.Disconnect(true);
                        client.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }

        }
    }
}
