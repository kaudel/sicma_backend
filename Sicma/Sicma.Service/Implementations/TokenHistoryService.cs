using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sicma.DTO.Request.Token;
using Sicma.DTO.Response;
using Sicma.DTO.Response.Tokens;
using Sicma.DTO.Response.Users;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;
using Sicma.Service.Interfaces;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Sicma.Service.Implementations
{
    public class TokenHistoryService:ITokenHistoryService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private ITokenHistoryRepository _tokenHistoryRepository;
        private IUserRepository _UserRepository;

        public TokenHistoryService(ITokenHistoryRepository repo, IUserRepository userRepository,
            IMapper mapper, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _mapper = mapper;
            _tokenHistoryRepository = repo;
            _UserRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<BaseResponse> ExistsTokenHistory( TokenRefreshRequest request, string userId)
        {
            var result = new BaseResponse();

            var resultQry = await _tokenHistoryRepository.GetAllAsync(
                predicate: p => p.Token == request.ExpiredToken &&
                                p.RefreshToken == request.RefreshToken &&
                                p.CreatedUserId == userId &&
                                !p.IsRevoked,   
                selector: p => new TokenHistory()
                {
                    Id = p.Id,
                    CreatedUserId = p.CreatedUserId,
                    RefreshToken = p.RefreshToken,
                    Token = p.Token,
                    CreatedDate = p.CreatedDate,
                    ExpirationDate = p.ExpirationDate,
                    IsActive = p.IsActive
                }) ;

            if (resultQry == null || resultQry.Count==0)
            {
                result.Success = false;
                result.Message = "TokenHistory not found";
                return result;
            }

            result.Success = true;
            return result;
        }


        public async Task<BaseResponse<TokenResponse>> CreateAccessToken(UserAutenticateResponse userAuth)
        {
            var response = new BaseResponse<TokenResponse>();
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["APISettings:secretKey"]!));

            AppUser user = await _UserRepository.FindByIdAsync(userAuth.Id);

            var roles = await _userManager.GetRolesAsync(user);

            List<Claim> claims =
                [
                new (JwtRegisteredClaimNames.Sub, user.Id),
                new( ClaimTypes.NameIdentifier, user.Id),
                new (JwtRegisteredClaimNames.Email, user.Email!),
                new (JwtRegisteredClaimNames.Name, user.UserName!),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new (JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),ClaimValueTypes.Integer64),
                //..roles.Select( r=> new Claim(ClaimTypes.Role, r))
                ];

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var credentails = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt16(_configuration["JWT:ExpirationMinutes"])),
                SigningCredentials = credentails,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            TokenResponse userResponse = new TokenResponse()
            {
                Token = tokenHandler.WriteToken(token),
            };

            response.Success = true;
            response.Message = "User authenticated";
            response.Data = userResponse;

            return response;
        }

        public async Task<BaseResponse<TokenResponse>> CreateRefreshToken(TokenRefreshRequest request, string userId)
        {
            var result = new BaseResponse<TokenResponse>();

            try
            {
                string refreshToken = GenerateRefreshToken();
                int expirationTime = Convert.ToInt16(_configuration["JWT:RefreshExpirationMinutes"]);

                TokenHistory token = new TokenHistory()
                {
                    CreatedUserId = userId,
                    CreatedDate = DateTime.UtcNow,
                    RefreshToken = refreshToken,
                    Token = request.ExpiredToken,
                    ExpirationDate = DateTime.UtcNow.AddMinutes(2),
                };

                var tokenResult = await _tokenHistoryRepository.AddAsync(token);

                if (tokenResult == null)
                {
                    result.Success = false;
                    result.Message = "Error creating token";
                    return result;
                }

                result.Data = _mapper.Map<TokenResponse>(tokenResult);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<BaseResponse> InvalidateToken(TokenRefreshRequest request, string userId)
        {
            var result = new BaseResponse<BaseResponse>();

            try
            {
                var tokenHistory = await GetTokenHistory(request, userId);

                await _tokenHistoryRepository.DeleteRevokeAsync(tokenHistory.Data!.Id);

                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<BaseResponse<TokenHistory>> GetTokenHistory(TokenRefreshRequest request, string userId)
        {
            var result = new BaseResponse<TokenHistory>();

            try
            {
                var resultQry = await _tokenHistoryRepository.GetAllAsync(
                    predicate: p => p.Token == request.ExpiredToken &&
                        p.RefreshToken == request.RefreshToken &&
                        p.CreatedUserId == userId
                    ,
                selector: p => new TokenHistory()
                {
                    Id = p.Id,
                    CreatedUserId = p.CreatedUserId,
                    RefreshToken = p.RefreshToken,
                    Token = p.Token,
                    CreatedDate = p.CreatedDate,
                    ExpirationDate = p.ExpirationDate,
                    IsActive = p.IsActive
                });

                if (resultQry == null || resultQry.Count == 0)
                {
                    result.Success = false;
                    result.Message = "TokenHistory record not found";
                }

                result.Success = true;
                result.Data = resultQry.FirstOrDefault();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;                
            }

            return result;
        }


        private string GenerateRefreshToken()
        {
            var byteArray = new byte[64];
            var refreshToken = "";

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteArray);
                refreshToken = Convert.ToBase64String(byteArray);
            }
            return refreshToken;
        }

    }
}
