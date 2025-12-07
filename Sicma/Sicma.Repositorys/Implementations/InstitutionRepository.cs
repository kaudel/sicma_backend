using Sicma.DataAccess.Context;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;

namespace Sicma.Repositorys.Implementations
{
    public class InstitutionRepository:BaseRepository<Institution>, IInstitutionRepository
    {
        protected readonly DbSicmaContext _dbContext;

        public InstitutionRepository( DbSicmaContext context): base(context) 
        {
            _dbContext = context;
        }
    }
}
