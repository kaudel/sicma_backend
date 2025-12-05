using Microsoft.AspNetCore.Identity;

namespace Sicma.Entities
{
    public class AppUser: IdentityUser 
    {
        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; }

        public string CreatedUserId { get; set; }
    }
}
