namespace SafeEntry.Persistance.Models
{
    public class EventInvitationModel
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? EventId { get; set; }
        public string? InvitationCode { get; set; }
        public bool Active { get; set; }
    }
}
