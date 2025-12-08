using Sicma.Entities;

namespace Sicma.Repositorys.Interfaces
{
    public interface IUserRepository: IBaseRepository<AppUser>
    {
        public bool IsUnique(string userName);
    }
}
