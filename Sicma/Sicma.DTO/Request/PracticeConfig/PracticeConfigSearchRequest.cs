namespace Sicma.DTO.Request.PracticeConfig
{
    public class PracticeConfigSearchRequest: PaginationRequest
    {
        public string? Name { get; set; } = null;
    }
}
