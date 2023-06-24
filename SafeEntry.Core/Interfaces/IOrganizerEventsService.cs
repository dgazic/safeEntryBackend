using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Core.Responses;

namespace SafeEntry.Core.Interfaces
{
    public interface IOrganizerEventsService
    {
        Task<OrganizerEventsResponse> GetOrganizerEvents(int id);
        Task<OrganizerEventsResponse> GetEvent(int eventId);
        Task<ScanQrCodeResponse> ScanQrCodeOnEvent(ScanEventInvitationDto request);
        Task<OrganizerEventsResponse> GetInvitedPeopleEvent(int eventId);
        Task<OrganizerEventsResponse> EnableDisableInvitation(int guestId);
    }
}
