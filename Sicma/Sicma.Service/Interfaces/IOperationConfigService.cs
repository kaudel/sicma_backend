using Sicma.DTO.Request.OperationConfig;
using Sicma.DTO.Response;
using Sicma.DTO.Response.OperationConfig;

namespace Sicma.Service.Interfaces
{
    public interface IOperationConfigService
    {
        Task<BaseResponse> Create(OperationConfigRequest request, string userId);
        Task<BaseResponse> Delete(string operationConfigId);
        Task<PaginationResponse<ListOperationConfigResponse>> GetAll(OperationConfigSearchRequest request);
        Task<BaseResponse<OperationConfigResponse>> GetById(string id);
        Task<BaseResponse> Update(string id, OperationConfigRequest request);
    }
}
