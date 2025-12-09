using Sicma.DataAccess.Context;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;

namespace Sicma.Repositorys.Implementations
{
    public class UserRepository: BaseRepository<AppUser>, IUserRepository
    {
        protected readonly DbSicmaContext sicmaContext;

        public UserRepository(DbSicmaContext context):base(context)
        {
            sicmaContext = context;
        }

        public bool IsUnique(string userName)
        {
            var resultRecord = sicmaContext.Users.FirstOrDefault(x => x.UserName == userName);

            if (resultRecord == null)
                return true;

            return false;
        }

        public AppUser GetUserByUserName(string userName)
        {
            var result = sicmaContext.Users.FirstOrDefault( x=> x.UserName == userName);

            return result;
        }
    }
}
