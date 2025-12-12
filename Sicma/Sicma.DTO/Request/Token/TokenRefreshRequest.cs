namespace Sicma.DTO.Request.Token
{
    public class TokenRefreshRequest
    {
        public string ExpiredToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
