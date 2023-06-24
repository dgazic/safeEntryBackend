using SafeEntry.Persistance.Models;

namespace SafeEntry.Persistance.Interfaces
{
    public interface IEventPersistance : IAsyncPersistance<EventModel>
    {
        public Task<EventModel> InsertInvitationTokenEvent(EventInvitationModel model);
        public Task<EventModel> GetEventById(int eventId);
        public Task<EventModel> ScanQrCodeOnEvent(ScanEventInvitationModel model);
        public Task<IEnumerable<EventInvitationModel>> GetInvitedPeopleEvent(int eventId);

        public Task<EventInvitationModel> EnableDisableInvitation(int guestId);


    }
}
