using Sicma.DTO.Request.User;
using Sicma.DTO.Response;
using Sicma.DTO.Response.Users;

namespace Sicma.Service.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse> Register(UserRequest request);
        Task<PaginationResponse<ListUsersResponse>> GetAll(UserSearchRequest request);
        Task<BaseResponse> Delete(string id);
        Task<BaseResponse> Update(string id, UserRequest request);
        Task<BaseResponse<UserResponse>> GetById(string id);

    }
}
