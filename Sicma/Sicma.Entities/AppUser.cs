using Microsoft.AspNetCore.Identity;
using Sicma.Entities.Interfaces;

namespace Sicma.Entities
{
    public class AppUser: IdentityUser, IBaseEntity  
    {
        public string FullName { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public string CreatedUserId { get; set; }
    }
}
