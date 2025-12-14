using Sicma.DataAccess.Context;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;

namespace Sicma.Repositorys.Implementations
{
    public class PracticeConfigRepository:BaseRepository<PracticeConfig>, IPracticeConfigRepository
    {
        protected readonly DbSicmaContext _context;
        public PracticeConfigRepository( DbSicmaContext context):base(context) 
        {
            _context = context;
        }
    }
}
