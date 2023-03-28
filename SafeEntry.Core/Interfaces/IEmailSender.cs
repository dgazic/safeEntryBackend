using SafeEntry.Core.Services.EmailService;

namespace SafeEntry.Core.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
