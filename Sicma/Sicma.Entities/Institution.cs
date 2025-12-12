using System.ComponentModel.DataAnnotations;

namespace Sicma.Entities
{
    public class Institution: BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
