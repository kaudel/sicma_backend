using System.ComponentModel.DataAnnotations;

namespace Sicma.Entities
{
    public class Institution: BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}
