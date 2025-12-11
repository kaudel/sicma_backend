namespace Sicma.Entities
{
    public class Exercise:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string OperationConfigId { get; set; }
        public string TrainingTypeId { get; set; }
        public OperationConfig OperationConfig { get; set; }
        public TrainingType TrainingType { get; set; }

    }
}
