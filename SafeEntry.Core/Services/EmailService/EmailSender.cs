using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SafeEntry.Core.Interfaces;
using SafeEntry.Core.Utils;
using SafeEntry.Persistance.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.NetworkInformation;

namespace SafeEntry.Core.Services.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly IConfiguration _configuration;


        public EmailSender(EmailConfiguration emailConfign, IConfiguration configuration)
        {
            _emailConfig = emailConfign;
            _configuration = configuration;
        }

        public void SendEmail(UserModel userModel, string subject, string htmlContent, string qrcode)
        {
            var key = _configuration["AppSettings:SendGridAPIKEY"];
            Execute(userModel,subject,htmlContent,key,qrcode).Wait();
        }
        static async Task Execute(UserModel userModel,string subject, string htmlContent, string key,string qrcode)
        {
            Attachment attachment = new Attachment();
            if(qrcode != null)
            {
                attachment.Disposition = "attachment";
                attachment.Content = qrcode;
                attachment.Filename = "Pozivnica";
                attachment.Type = "image/png";
            }
            
            var client = new SendGridClient(key);
            var from = new EmailAddress("safeentrycompany@gmail.com", "Safe entry support");
            var to = new EmailAddress(userModel.Email, userModel.LastName + " " + userModel.FirstName);
            var plainTextContent = "and easy to do anywhere, even with C#";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            if(qrcode != null)
            {
                msg.AddAttachment(attachment);

            }
            var response = await client.SendEmailAsync(msg);
        }
    }
}
