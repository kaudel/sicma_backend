using Sicma.DTO.Request.Institution;
using Sicma.DTO.Response;
using Sicma.DTO.Response.Institutions;

namespace Sicma.Service.Interfaces
{
    public interface IInstitutionService
    {
        Task<BaseResponse> Create(InstitutionRequest request);
        Task<PaginationResponse<ListInstitutionsResponse>> GetAll(InstitutionSearchRequest request);
        Task<BaseResponse<InstitutionResponse>> GetById(string id);
        Task<BaseResponse> Delete(string institutionId);
        Task<BaseResponse> Update(string id, InstitutionRequest request);
    }
}
