using AutoMapper;
using SafeEntry.Core.Interfaces;
using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Core.Responses;
using SafeEntry.Persistance.Interfaces;
using SafeEntry.Persistance.Models;

namespace SafeEntry.Core.Services
{
    public class OrganizerEventService : IOrganizerEventsService
    {
        private readonly IEventPersistance _eventPersistance;
        private readonly IMapper _mapper;
        public OrganizerEventService(IEventPersistance eventPersistance, IMapper mapper)
        {
            _eventPersistance = eventPersistance;
            _mapper = mapper;
        }

        public async Task<OrganizerEventsResponse> GetOrganizerEvents(int id)
        {
            var validate = await Validate(id);
            var response = new OrganizerEventsResponse();
            if (validate.Success)
            {
                var events = await _eventPersistance.GetAll(id);
                response.EventsDto = _mapper.Map<IEnumerable<EventModel>, List<EventDto>>(events);
            }
            return response;
        }

        private async Task<OrganizerEventsResponse> Validate(int id)
        {
            var response = new OrganizerEventsResponse();


            return response;
        }
    }
}
