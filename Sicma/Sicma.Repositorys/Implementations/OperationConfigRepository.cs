using Sicma.DataAccess.Context;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;

namespace Sicma.Repositorys.Implementations
{
    public class OperationConfigRepository : BaseRepository<OperationConfig>, IOperationConfigRepository
    {
        protected readonly DbSicmaContext _sicmaContext;

        public OperationConfigRepository(DbSicmaContext context): base(context) 
        {
            _sicmaContext = context;
        }


    }
}
