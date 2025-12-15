using System.ComponentModel.DataAnnotations;

namespace Sicma.Entities
{
    public class TrainingType:BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
