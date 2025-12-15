using Sicma.DTO.Request.Token;
using Sicma.DTO.Response;
using Sicma.DTO.Response.Tokens;
using Sicma.DTO.Response.Users;

namespace Sicma.Service.Interfaces
{
    public interface ITokenHistoryService
    {
        Task<BaseResponse<TokenResponse>> CreateAccessToken(UserAutenticateResponse userAuth);
        Task<BaseResponse<TokenResponse>> CreateRefreshToken(TokenRefreshRequest request, Guid userId);
        Task<BaseResponse> ExistsTokenHistory(TokenRefreshRequest request, Guid userId);
        Task<BaseResponse> InvalidateToken(TokenRefreshRequest request, Guid userId);
    }
}
