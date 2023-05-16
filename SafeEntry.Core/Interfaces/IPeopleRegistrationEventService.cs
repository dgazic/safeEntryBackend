using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Core.Responses;

namespace SafeEntry.Core.Interfaces
{
    public interface IPeopleRegistrationEventService
    {
        Task<PeopleRegistrationEventResponse> PeopleRegistrationOnEvent(PeopleRegistrationEventDto request);
    }
}
