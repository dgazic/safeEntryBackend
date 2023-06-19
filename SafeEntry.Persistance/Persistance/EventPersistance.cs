using Dapper;
using SafeEntry.Persistance.Context;
using SafeEntry.Persistance.Interfaces;
using SafeEntry.Persistance.Models;

namespace SafeEntry.Persistance.Persistance
{
    public class EventPersistance : IEventPersistance
    {
        private readonly DapperContext _context;
        public EventPersistance(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventModel>> GetAll(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    OrganizerId = id
                };
                var events = await connection.QueryAsync<EventModel>("SELECT * FROM get_events(@OrganizerId)", parameters);
                return events;
            }
        }

        public async Task<EventModel> GetEventById(int eventId)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    EventId = eventId
                };
                var _event = await connection.QueryAsync<EventModel>("SELECT * FROM get_event_by_id(@EventId)", parameters);
                return _event.FirstOrDefault();
            }
        }

        public async Task<IEnumerable<EventInvitationModel>> GetInvitedPeopleEvent(int eventId)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    EventId = eventId
                };
                var invitedPeople = await connection.QueryAsync<EventInvitationModel>("SELECT * FROM get_invited_people_event(@EventId)", parameters);
                return invitedPeople;
            }
        }

        public async Task<EventModel> Insert(EventModel model)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    Name = model.Name,
                    Description = model.Description,
                    Address = model.Address,
                    EventStarts = model.EventStarts,
                    OrganizerId = model.OrganizerId,
                };

                var eventModel = await connection.QueryAsync<EventModel>("CALL EventRegistration_Insert(@Name,@Description,@Address, @EventStarts,@OrganizerId)", parameters);
                return eventModel.FirstOrDefault();
            }
        }

        public async Task<EventModel> InsertInvitationTokenEvent(EventInvitationModel model)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    EventId = model.EventId,
                    FirstName= model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    InvitationCode = model.InvitationCode,
                    Active = true
                };
                var userModel = await connection.QueryAsync<EventModel>("CALL EventInvitation_Insert(@EventId,@FirstName,@LastName,@Email,@PhoneNumber, @InvitationCode, @Active)", parameters);
                return userModel.FirstOrDefault();
            }
        }

        public async Task<EventModel> InsertPeopleToEvent(EventInvitationModel model, string firstName, string lastName)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    FirstName = firstName,
                    LastName = lastName,
                    EventId = model.EventId,
                };
                var eventModel = await connection.QueryAsync<EventModel>("CALL PeopleToEvent_Insert(@FirstName,@LastName,@EventId)", parameters);
                return eventModel.FirstOrDefault();
            }
        }

        public async Task<EventModel> ScanQrCodeOnEvent(ScanEventInvitationModel model)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                   InvitationCode = model.InvitationCode,
                   EventId = model.EventId
                };
                var qrCode = await connection.QueryAsync<EventModel>("SELECT * FROM check_invitation(@EventId, @InvitationCode)", parameters);
                return qrCode.FirstOrDefault();
            }
        }
    }
}
