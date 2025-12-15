using Sicma.Entities;

namespace Sicma.DTO.Response.Users
{
    public class UserAutenticateResponse
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}
