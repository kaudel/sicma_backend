using System.ComponentModel.DataAnnotations;

namespace Sicma.Entities
{
    public class PracticeConfig: BaseEntity
    {
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public Guid OperationConfigId { get; set; }
        public Guid TrainingTypeId { get; set; }
        public OperationConfig OperationConfig { get; set; }
        public TrainingType TrainingType { get; set; }

    }
}
