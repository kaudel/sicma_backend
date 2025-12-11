namespace Sicma.DTO.Request.OperationConfig
{
    public class OperationConfigSearchRequest:PaginationRequest
    {
        public string? OperationName { get; set; } = null;
    }
}
