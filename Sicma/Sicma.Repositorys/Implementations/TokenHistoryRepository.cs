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
    }
}
