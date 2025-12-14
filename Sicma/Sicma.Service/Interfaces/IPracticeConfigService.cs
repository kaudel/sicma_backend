using Sicma.DTO.Request.PracticeConfig;
using Sicma.DTO.Response;
using Sicma.DTO.Response.PracticeConfig;

namespace Sicma.Service.Interfaces
{
    public interface IPracticeConfigService
    {
        Task<BaseResponse> Create(PracticeConfigRequest request, string userId);
        Task<BaseResponse> Delete(string practiceConfigId);
        Task<BaseResponse> Update(string id, PracticeConfigRequest request);
        Task<PaginationResponse<ListPracticeConfigResponse>> GetAll(PracticeConfigSearchRequest request);
        Task<BaseResponse<PracticeConfigResponse>> GetById(string id);
    }
}
