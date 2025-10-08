namespace Sicma.DTO.Request.User
{
    public class UserSearchRequest: PaginationRequest
    {
        public string? FullName { get; set; } = null!;

        public string? Email { get; set; } = null!;

        public string? Institution { get; set; } = null!;
    }
}
