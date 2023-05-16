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

        public async Task<PeopleRegistrationEventResponse> PeopleRegistrationOnEvent(PeopleRegistrationEventDto request)
        {
            var response = await Validate(request);

            if (response.Success)
            {
                QRCodeModel qrCodeModel = InvitationQRCodeGenerator.GenerateQRCodeInvitation(request);
                UserModel userModel= new UserModel();
                userModel.Email = request.Email;
                var peopleRegistrationEventModel = new PeopleRegistrationEventModel { EventId = request.EventId, InvitationToken = qrCodeModel.ShaDataEncoded };
                await _eventPersistance.InsertInvitationTokenEvent(peopleRegistrationEventModel);
                await _eventPersistance.InsertPeopleToEvent(peopleRegistrationEventModel, request.Name, request.Surname);

                string htmlContent, subject;
                subject = "SafeEntryCompany - pozivnica za privatnu zabavu";
                htmlContent = $"Poštovani,<br> Molimo Vas da na ulazu pri ulasku na događaj pokažete dobiveni barkod<br><img src=\"data:image/png;base64,{qrCodeModel.Base64Image}\" width=\"100\" height=\"100\" />";
                _emailSender.SendEmail(userModel, subject, htmlContent);
            }
            return response;
        }

        private async Task<PeopleRegistrationEventResponse> Validate(PeopleRegistrationEventDto request)
        {
            var response = new PeopleRegistrationEventResponse();


            return response;
        }
    }
}
