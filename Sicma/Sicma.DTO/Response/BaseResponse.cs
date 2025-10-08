namespace Sicma.DTO.Response
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? ErrorCode { get; set; }
    }

    public class BaseResponse<T>:BaseResponse
    {
        public T? Data { get; set; }
    }
}
