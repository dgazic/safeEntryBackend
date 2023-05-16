using SafeEntry.Core.Responses;

namespace SafeEntry.Core.Interfaces
{
    public interface IOrganizerEventsService
    {
        Task<OrganizerEventsResponse> GetOrganizerEvents(int id);
    }
}
