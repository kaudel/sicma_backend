using Sicma.Entities;

namespace Sicma.Repositorys.Interfaces
{
    public interface IUserRepository: IBaseRepository<AppUser>
    {
        bool IsUnique(string userName);
        AppUser GetUserByUserName(string userName);
    }
}
