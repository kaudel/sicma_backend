namespace Sicma.DTO.Request.User
{
    public class UserRequest
    {
        public string Nickname { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Institution { get; set; } = null!;

        public int UserTypeId { get; set; }
    }
}
