namespace Sicma.DTO.Request.Classroom
{
    public class ClassroomRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid InstitutionId { get; set; }
        public Guid PracticeConfigId { get; set; }
    }
}
