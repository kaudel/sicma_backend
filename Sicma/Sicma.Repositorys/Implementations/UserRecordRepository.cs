using Sicma.DataAccess.Context;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;

namespace Sicma.Repositorys.Implementations
{
    public class UserRecordRepository:BaseRepository<UserRecord>, IUserRecordRepository
    {
        protected readonly DbSicmaContext _dbContext;

        public UserRecordRepository(DbSicmaContext context):base(context)         
        {
            _dbContext = context;
        }
    }
}
