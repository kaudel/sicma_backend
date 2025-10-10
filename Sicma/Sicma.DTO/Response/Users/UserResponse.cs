namespace Sicma.DTO.Response.Users
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Nickname { get; set; } = default!;

        public string FullName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string Institution { get; set; } = null!;

        public int UserTypeId { get; set; }
    }
}
