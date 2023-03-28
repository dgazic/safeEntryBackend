namespace SafeEntry.Core.Models.DtoModels
{
    public class PeopleRegistrationEventDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int EventId { get; set; }

    }
}
