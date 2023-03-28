using SafeEntry.Persistance.Models;

namespace SafeEntry.Persistance.Interfaces
{
    public interface IEventPersistance : IAsyncPersistance<EventModel>
    {
        public Task<EventModel> InsertInvitationTokenEvent(PeopleRegistrationEventModel userModel);
        public Task<EventModel> InsertPeopleToEvent(PeopleRegistrationEventModel userModel, string firstName, string lastName);
    }
}
