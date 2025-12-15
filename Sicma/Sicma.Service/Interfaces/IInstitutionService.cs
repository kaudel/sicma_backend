using Sicma.DTO.Request.Institution;
using Sicma.DTO.Response;
using Sicma.DTO.Response.Institutions;

namespace Sicma.Service.Interfaces
{
    public interface IInstitutionService
    {
        Task<BaseResponse> Create(InstitutionRequest request, Guid userId);
        Task<PaginationResponse<ListInstitutionsResponse>> GetAll(InstitutionSearchRequest request);
        Task<BaseResponse<InstitutionResponse>> GetById(Guid id);
        Task<BaseResponse> Delete(Guid institutionId);
        Task<BaseResponse> Update(Guid id, InstitutionRequest request);
    }
}
