using Sicma.DTO.Request.OperationConfig;
using Sicma.DTO.Response;
using Sicma.DTO.Response.OperationConfig;

namespace Sicma.Service.Interfaces
{
    public interface IOperationConfigService
    {
        Task<BaseResponse> Create(OperationConfigRequest request, Guid userId);
        Task<BaseResponse> Delete(Guid operationConfigId);
        Task<PaginationResponse<ListOperationConfigResponse>> GetAll(OperationConfigSearchRequest request);
        Task<BaseResponse<OperationConfigResponse>> GetById(Guid id);
        Task<BaseResponse> Update(Guid id, OperationConfigRequest request);
    }
}
