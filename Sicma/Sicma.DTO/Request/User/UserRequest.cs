namespace Sicma.DTO.Request.User
{
    public class UserRequest
    {
        public string UserName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public Guid InstitutionId { get; set; } 
        public string UserRole { get; set; }
    }
}
