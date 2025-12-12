using AutoMapper;
using Sicma.DTO.Response.Tokens;
using Sicma.Entities;

namespace Sicma.Service.Mappers
{
    public class TokenHistoryMap:Profile
    {
        public TokenHistoryMap()
        {
            CreateMap<TokenHistory, TokenResponse>();                        
        }
    }
}
