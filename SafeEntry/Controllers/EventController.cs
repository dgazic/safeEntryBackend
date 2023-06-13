using Microsoft.AspNetCore.Mvc;
using SafeEntry.Core.Interfaces;
using SafeEntry.Core.Models.DtoModels;

namespace SafeEntry.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IPeopleRegistrationEventService _peopleRegistrationEventService;
        private readonly IEventRegistrationService _eventRegistrationService;
        private readonly IOrganizerEventsService _organizerEventService;

        public EventController(IPeopleRegistrationEventService peopleRegistrationEventService, IEventRegistrationService eventRegistrationService, IOrganizerEventsService organizerEventsService)
        {
            
            _peopleRegistrationEventService = peopleRegistrationEventService;
            _eventRegistrationService = eventRegistrationService;
            _organizerEventService= organizerEventsService;
        }


        [HttpPost("EventRegistration")]
        public async Task<IActionResult> EventRegistration(EventRegistrationDto request)
        {
            var response = await _eventRegistrationService.EventRegistration(request);
            return Ok();
        }

        [HttpPost("PeopleRegistrationEvent")]
        public async Task<IActionResult> PeopleRegistrationEvent(PeopleRegistrationEventDto request)
        {
            var response = await _peopleRegistrationEventService.PeopleRegistrationOnEvent(request);
            return null;
        }
        [HttpGet("GetEvents/{organizerId}")]
        public async Task<IActionResult> GetOrganizerEvents(int organizerId)
        {
            var response = await _organizerEventService.GetOrganizerEvents(organizerId);
            return Ok(response);
        }
    }
}
