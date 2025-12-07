namespace Sicma.DTO.Request.Institution
{
    public class InstitutionSearchRequest: PaginationRequest
    {
        public string? Name { get; set; } = null!;
    }
}
