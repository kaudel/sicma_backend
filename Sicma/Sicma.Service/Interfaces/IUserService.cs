using Sicma.DTO.Request.User;
using Sicma.DTO.Response;

namespace Sicma.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse> Register(UserRequest request);

    }
}
