using Sicma.DTO.Request.PracticeConfig;
using Sicma.DTO.Response;
using Sicma.DTO.Response.PracticeConfig;

namespace Sicma.Service.Interfaces
{
    public interface IPracticeConfigService
    {
        Task<BaseResponse> Create(PracticeConfigRequest request, Guid userId);
        Task<BaseResponse> Delete(Guid practiceConfigId);
        Task<BaseResponse> Update(Guid id, PracticeConfigRequest request);
        Task<PaginationResponse<ListPracticeConfigResponse>> GetAll(PracticeConfigSearchRequest request);
        Task<BaseResponse<PracticeConfigResponse>> GetById(Guid id);
    }
}
