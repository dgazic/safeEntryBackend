namespace SafeEntry.Core.Models.DtoModels
{
    public class EventRegistrationDto
    {
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        
        public int EventOrganizer { get; set; }
    }
}
