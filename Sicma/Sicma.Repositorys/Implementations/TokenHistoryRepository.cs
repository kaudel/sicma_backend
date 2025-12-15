using Microsoft.EntityFrameworkCore;
using Sicma.DataAccess.Context;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;

namespace Sicma.Repositorys.Implementations
{
    public class TokenHistoryRepository:BaseRepository<TokenHistory>, ITokenHistoryRepository
    {
        protected readonly DbSicmaContext _dbSicmaContext;

        public TokenHistoryRepository(DbSicmaContext context):base(context)
        {
            _dbSicmaContext = context;
        }

        public async Task DeleteRevokeAsync(Guid tokenId)
        {
            await _dbSicmaContext.TokenHistory
                    .Where(p => p.Id == tokenId)
                    .ExecuteUpdateAsync(
                        p => p.SetProperty(p => p.IsRevoked, true)
                        );
        }
    }
}
