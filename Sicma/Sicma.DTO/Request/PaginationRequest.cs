namespace Sicma.DTO.Request
{
    public class PaginationRequest
    {
        public int Page { get; set; } = 1;

        public int Rows { get; set; } = 10;
    }
}
