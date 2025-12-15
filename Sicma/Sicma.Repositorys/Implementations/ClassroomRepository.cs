using Sicma.DataAccess.Context;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;

namespace Sicma.Repositorys.Implementations
{
    public class ClassroomRepository:BaseRepository<Classroom>, IClassroomRepository
    {
        public readonly DbSicmaContext _dbContext;
        public ClassroomRepository(DbSicmaContext context):base(context) 
        {
            _dbContext = context;
        }
    }
}
