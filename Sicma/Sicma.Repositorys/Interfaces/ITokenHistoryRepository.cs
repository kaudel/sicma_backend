using Sicma.Entities;

namespace Sicma.Repositorys.Interfaces
{
    public interface ITokenHistoryRepository:IBaseRepository<TokenHistory>
    {
        Task DeleteRevokeAsync(string tokenId);
    }
}
