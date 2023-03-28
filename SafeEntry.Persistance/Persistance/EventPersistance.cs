using Dapper;
using SafeEntry.Persistance.Context;
using SafeEntry.Persistance.Interfaces;
using SafeEntry.Persistance.Models;
using System.Data;
using System.Reflection;

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

        public async Task<EventModel> Insert(EventModel model)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    Name = model.Name,
                    Description = model.Description,
                    OrganizerId = model.OrganizerId,
                };

                var eventModel = await connection.QueryAsync<EventModel>("CALL EventRegistration_Insert(@Name,@Description,@OrganizerId)", parameters);
                return eventModel.FirstOrDefault();
            }
        }

        public async Task<EventModel> InsertInvitationTokenEvent(PeopleRegistrationEventModel model)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new
                {
                    EventId = model.EventId,
                    InvitationToken = model.InvitationToken
                };
                var userModel = await connection.QueryAsync<EventModel>("CALL InvitationTokenEvent_Insert(@EventId,@InvitationToken)", parameters);
                return userModel.FirstOrDefault();
            }
        }

        public async Task<EventModel> InsertPeopleToEvent(PeopleRegistrationEventModel model, string firstName, string lastName)
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


    }
}
