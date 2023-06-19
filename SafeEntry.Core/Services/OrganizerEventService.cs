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

        public async Task<OrganizerEventsResponse> GetEvent(int eventId)
        {
            var validate = await Validate(eventId);
            var response = new OrganizerEventsResponse();
            if (validate.Success)
            {
                var _event = await _eventPersistance.GetEventById(eventId);
                response.EventDto = _mapper.Map<EventDto>(_event);
            }
            return response;
        }

        public async Task<OrganizerEventsResponse> GetInvitedPeopleEvent(int eventId)
        {
            var validate = await Validate(eventId);
            var response = new OrganizerEventsResponse();
            if (validate.Success)
            {
                var invitedPeople = await _eventPersistance.GetInvitedPeopleEvent(eventId);
                response.EventsInvitationDto = _mapper.Map<IEnumerable<EventInvitationModel>, List<EventInvitationDto>>(invitedPeople);
            }
            return response;
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

        public async Task<ScanQrCodeResponse> ScanQrCodeOnEvent(ScanEventInvitationDto request)
        {
            var response = new ScanQrCodeResponse();
            var scanEventInvitationModel = new ScanEventInvitationModel
            {
                EventId = request.EventId,
                InvitationCode = request.InvitationCode
            };
            var eventInvitationResponse = await _eventPersistance.ScanQrCodeOnEvent(scanEventInvitationModel);
            if(eventInvitationResponse == null)
                response.Success = false;
            return response;
            
        }

        private async Task<OrganizerEventsResponse> Validate(int id)
        {
            var response = new OrganizerEventsResponse();
            return response;
        }
    }
}
