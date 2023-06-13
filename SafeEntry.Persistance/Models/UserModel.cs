namespace SafeEntry.Persistance.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public byte[] SaltPassword { get; init; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int UserRoleId { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
