using System.ComponentModel.DataAnnotations;

namespace Sicma.Entities
{
    public class TrainingType:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
