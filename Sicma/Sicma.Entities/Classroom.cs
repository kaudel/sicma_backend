using System.ComponentModel.DataAnnotations;

namespace Sicma.Entities
{
    public class Classroom:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string  InstitutionId { get; set; }
        public Institution Institution { get; set; }

    }
}
