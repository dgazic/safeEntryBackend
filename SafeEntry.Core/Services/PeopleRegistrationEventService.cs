using SafeEntry.Core.Interfaces;
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
                string invitationKeyToken = InvitationQRCodeGenerator.GenerateQRCodeInvitation(request);
                var message = new Message(new string[] { request.Email }, "SafeEntryCompany - pozivnica za privatnu zabavu", $"Poštovani,<br> Molimo Vas da na ulazu pri ulasku u zabavu pokažete dobiveni barkod za ulaz" +
                    $"<br> Link");
                var peopleRegistrationEventModel = new PeopleRegistrationEventModel { EventId = request.EventId, InvitationToken = invitationKeyToken };
                await _eventPersistance.InsertInvitationTokenEvent(peopleRegistrationEventModel);
                await _eventPersistance.InsertPeopleToEvent(peopleRegistrationEventModel, request.Name, request.Surname);
                //_eventPersistance.Insert();
                //_emailSender.SendEmail(message);
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
