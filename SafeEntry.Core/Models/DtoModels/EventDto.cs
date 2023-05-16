namespace SafeEntry.Core.Models.DtoModels
{
    public class EventDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrganizerId { get; set; }

        public string? InvitationToken { get; set; }
    }
}
