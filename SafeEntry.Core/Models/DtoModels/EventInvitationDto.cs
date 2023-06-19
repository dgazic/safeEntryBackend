namespace SafeEntry.Core.Models.DtoModels
{
    public class EventInvitationDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? EventId { get; set; }
        public bool? Active { get; set; }

    }
}
