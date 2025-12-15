using System.ComponentModel.DataAnnotations;

namespace Sicma.Entities
{
    public class Classroom:BaseEntity
    {
        [Required][MaxLength(100)]
        public string Name { get; set; }
        [Required] [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(50)]
        public Guid InstitutionId { get; set; }
        [MaxLength(50)]
        public Guid PracticeConfigId { get; set; }
        public Institution Institution { get; set; }        
        public PracticeConfig PracticeConfig { get; set; }

    }
}
