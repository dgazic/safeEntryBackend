namespace SafeEntry.Core.Models.DtoModels
{
    public class UserModelDto
    {
        public int Id { get; init; }
        public string? LastName { get; init; }
        public string? FirstName { get; init; }
        public int? UserRoleId { get; init; }
        public string? PhoneNumber { get; init; }
        public byte[] ActivationToken { get; init; }
    }
}
