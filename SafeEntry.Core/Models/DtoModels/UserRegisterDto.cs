namespace SafeEntry.Core.Models.DtoModels
{
    public class UserRegisterDto
    {
        public string? CompanyName { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public int UserRoleId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
