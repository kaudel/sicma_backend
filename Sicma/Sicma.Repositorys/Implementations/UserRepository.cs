using Sicma.DataAccess.Context;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;

namespace Sicma.Repositorys.Implementations
{
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        protected readonly DbSicmaContext sicmaContext;

        public UserRepository(DbSicmaContext context):base(context)
        {
            sicmaContext = context;
        }
    }
}
