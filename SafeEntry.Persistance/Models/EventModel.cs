namespace SafeEntry.Persistance.Models
{
    public class EventModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string EventStarts { get; set; }
        public int OrganizerId { get; set; }
        public string CompanyName { get; set; }
        public int PeopleInvited { get; set; }
    }
}
