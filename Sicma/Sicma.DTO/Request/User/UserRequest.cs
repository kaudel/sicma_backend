namespace Sicma.DTO.Request.User
{
    public class UserRequest
    {
        public string UserName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string InstitutionId { get; set; } = null!;
        public string UserRole { get; set; }
    }
}
