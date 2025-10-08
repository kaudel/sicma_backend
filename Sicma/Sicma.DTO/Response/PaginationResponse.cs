namespace Sicma.DTO.Response
{
    public class PaginationResponse<T>: BaseResponse
    {
        public ICollection<T> Data { get; set; } = new List<T>();
        public int TotalRows { get; set; }
        public int TotalPages { get; set; }
    }
}
