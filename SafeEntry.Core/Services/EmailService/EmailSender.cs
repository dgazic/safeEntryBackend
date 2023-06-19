using MimeKit;
using SafeEntry.Core.Interfaces;
using SafeEntry.Core.Utils;
using SafeEntry.Persistance.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace SafeEntry.Core.Services.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;


        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendEmail(UserModel userModel, string subject, string htmlContent)
        {
            Execute(userModel,subject,htmlContent).Wait();
        }
        static async Task Execute(UserModel userModel,string subject, string htmlContent)
        {
            var client = new SendGridClient("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");
            var from = new EmailAddress("safeentrycompany@gmail.com", "Safe entry support");
            var to = new EmailAddress(userModel.Email, userModel.LastName + " " + userModel.FirstName);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
