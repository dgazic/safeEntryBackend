using SafeEntry.Core.Models.DtoModels;

namespace SafeEntry.Core.Responses
{
    public class OrganizerEventsResponse: ResponseBase
    {
        public List<EventDto> EventsDto { get; set; }
        public EventDto EventDto { get; set; }
        public List<EventInvitationDto> EventsInvitationDto { get; set; }
    }
}
