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
            try
            {
                var response = await _eventRegistrationService.EventRegistration(request);
                return Ok(response) ;
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
        [HttpGet("GetEvents")]
        public async Task<IActionResult> GetOrganizerEvents(int organizerId)
        {
            try
            {
                var response = await _organizerEventService.GetOrganizerEvents(organizerId);
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
            
        }
        [HttpGet("EventById")]
        public async Task<IActionResult> GetEvent(int eventId)
        {
            try
            {
                var response = await _organizerEventService.GetEvent(eventId);
                return Ok(response.EventsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("PeopleRegistrationEvent")]
        public async Task<IActionResult> PeopleRegistrationEvent(EventInvitationDto request)
        {
            try
            {
                var response = await _peopleRegistrationEventService.PeopleRegistrationOnEvent(request);
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ScanQrCode")]
        public async Task<IActionResult> ScanQrCode(ScanEventInvitationDto request)
        {
            try
            {
                var response = await _organizerEventService.ScanQrCodeOnEvent(request);
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetInvitedPeopleEvent")]
        public async Task<IActionResult> GetInvitedPeopleEvent(int eventId)
        {
            try 
            {
                var response = await _organizerEventService.GetInvitedPeopleEvent(eventId);
                return Ok(response.EventsInvitationDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
