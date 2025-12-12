namespace Sicma.Entities
{
    public class TokenHistory:BaseEntity
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
