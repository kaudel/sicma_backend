using Sicma.DTO.Request.UserRecord;
using Sicma.DTO.Response;
using Sicma.DTO.Response.UserRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicma.Service.Interfaces
{
    public interface IUserRecordService
    {
        Task<BaseResponse> Create(UserRecordRequest request, Guid userId);
        Task<BaseResponse> Delete(Guid Id);
        Task<BaseResponse> Update(Guid id, UserRecordRequest request);
        Task<PaginationResponse<ListUserRecordResponse>> GetAll(UserRecordSearchRequest request);
        Task<BaseResponse<UserRecordResponse>> GetById(Guid id);
    }
}
