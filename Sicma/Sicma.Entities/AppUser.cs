using Microsoft.AspNetCore.Identity;
using Sicma.Entities.Interfaces;

namespace Sicma.Entities
{
    public class AppUser: IdentityUser<Guid>, IBaseEntity  
    {
        public string FullName { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? UpdatedDate { get; set; }
        public Guid CreatedUserId { get; set; }
    }
}
