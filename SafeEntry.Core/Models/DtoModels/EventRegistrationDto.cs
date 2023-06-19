namespace SafeEntry.Core.Models.DtoModels
{
    public class EventRegistrationDto
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public string EventAddress { get; set; }
        public string EventStarts { get; set; }
        
        public int EventOrganizerId { get; set; }
    }
}
