namespace Sicma.DTO.Request.Classroom
{
    public class ClassroomSearchRequest:PaginationRequest
    {
        public string? Name { get; set; } = null!;
    }
}
