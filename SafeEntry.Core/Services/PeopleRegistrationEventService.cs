using SafeEntry.Core.Interfaces;
using SafeEntry.Core.Models;
using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Core.Responses;
using SafeEntry.Core.Services.EmailService;
using SafeEntry.Core.Utils;
using SafeEntry.Persistance.Interfaces;
using SafeEntry.Persistance.Models;

namespace SafeEntry.Core.Services
{
    public class PeopleRegistrationEventService : IPeopleRegistrationEventService
    {
        private readonly IEmailSender _emailSender;
        private readonly IEventPersistance _eventPersistance;

        public PeopleRegistrationEventService(IEmailSender emailSender, IEventPersistance eventPersistance)
        {
            _emailSender = emailSender;
            _eventPersistance = eventPersistance;
        }

        public async Task<PeopleRegistrationEventResponse> PeopleRegistrationOnEvent(EventInvitationDto request)
        {
            var response = await Validate(request);

            if (response.Success)
            {
                QRCodeModel qrCodeModel = InvitationQRCodeGenerator.GenerateQRCodeInvitation(request);
                UserModel userModel= new UserModel();
                userModel.Email = request.Email;
                var eventInvitationModel = new EventInvitationModel { EventId = request.EventId, FirstName = request.FirstName, LastName = request.LastName, Email = request.Email, PhoneNumber = request.PhoneNumber, InvitationCode = qrCodeModel.ShaDataEncoded };
                await _eventPersistance.InsertInvitationTokenEvent(eventInvitationModel);

                string htmlContent, subject;
                subject = "SafeEntryCompany - pozivnica za privatnu zabavu";
                htmlContent = $"Poštovani,<br> Molimo Vas da na ulazu pri ulasku na događaj pokažete dobiveni barkod<br><img src=\"data:image/png;base64,{qrCodeModel.Base64Image}\" width=\"100\" height=\"100\" />";
                _emailSender.SendEmail(userModel, subject, htmlContent);
            }
            return response;
        }

        private async Task<PeopleRegistrationEventResponse> Validate(EventInvitationDto request)
        {
            var response = new PeopleRegistrationEventResponse();


            return response;
        }
    }
}
