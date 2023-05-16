namespace SafeEntry.Persistance.Models
{
    public class PeopleRegistrationEventModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int EventId { get; set; }

        public string? InvitationToken { get; set; }
    }
}
