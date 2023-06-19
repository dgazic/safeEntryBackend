using SafeEntry.Core.Models.DtoModels;
using SafeEntry.Core.Responses;
using SafeEntry.Persistance.Interfaces;
using SafeEntry.Persistance.Models;

namespace SafeEntry.Core.Interfaces
{
    public interface IEventRegistrationService
    {
        Task<EventRegistrationResponse> EventRegistration(EventRegistrationDto request);
    }
}
