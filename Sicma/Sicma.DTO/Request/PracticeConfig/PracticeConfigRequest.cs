using System.ComponentModel.DataAnnotations;

namespace Sicma.DTO.Request.PracticeConfig
{
    public class PracticeConfigRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public Guid OperationConfigId { get; set; }
        public Guid TrainingTypeId { get; set; }
    }
}
