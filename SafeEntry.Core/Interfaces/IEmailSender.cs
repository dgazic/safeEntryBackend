using SafeEntry.Core.Services.EmailService;
using SafeEntry.Persistance.Models;

namespace SafeEntry.Core.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(UserModel userModel,string subject,string htmlContent);
    }
}
