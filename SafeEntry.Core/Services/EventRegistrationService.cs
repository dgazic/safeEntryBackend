using SafeEntry.Core.Interfaces;
using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Core.Responses;
using SafeEntry.Persistance.Interfaces;
using SafeEntry.Persistance.Models;

namespace SafeEntry.Core.Services
{
    public class EventRegistrationService : IEventRegistrationService
    {
        private readonly IEventPersistance _eventPersistance;

        public EventRegistrationService(IEventPersistance eventPersistance)
        {
            _eventPersistance = eventPersistance;
        }


        public async Task<EventRegistrationResponse> EventRegistration(EventRegistrationDto request)
        {
            var response = await Validate(request);

            if (response.Success)
            {
                var eventModel = new EventModel
                {
                    Name = request.EventName,
                    Description = request.EventDescription,
                    Address = request.EventAddress,
                    EventStarts = request.EventStarts,
                    OrganizerId = request.EventOrganizerId
                };

                await _eventPersistance.Insert(eventModel);
            }
            return response;
        }
        private async Task<EventRegistrationResponse> Validate(EventRegistrationDto request)
        {
            var response = new EventRegistrationResponse();


            return response;
        }
    }
}
