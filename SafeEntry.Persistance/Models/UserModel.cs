namespace SafeEntry.Persistance.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public byte[]? Password { get; set; }
        public byte[]? SaltPassword { get; init; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int UserRoleId { get; set; }
        public int? Activated { get; init; }
    }
}
