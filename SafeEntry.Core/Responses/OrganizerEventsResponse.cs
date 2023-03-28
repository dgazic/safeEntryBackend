using SafeEntry.Core.Models.DtoModels;

namespace SafeEntry.Core.Responses
{
    public class OrganizerEventsResponse: ResponseBase
    {
        public List<EventDto> EventsDto { get; set; }
    }
}
