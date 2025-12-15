namespace Sicma.DTO.Response.Users
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Institution { get; set; } = null!;
        public Guid UserRoleId { get; set; }
    }
}
