using Sicma.DTO.Request.Classroom;
using Sicma.DTO.Response;
using Sicma.DTO.Response.Classroom;
using Sicma.DTO.Response.Institutions;

namespace Sicma.Service.Interfaces
{
    public interface IClassroomService
    {
        Task<BaseResponse> Create(ClassroomRequest request, Guid userId);
        Task<BaseResponse> Delete(Guid classroomId);
        Task<BaseResponse> Update(Guid id, ClassroomRequest request);
        Task<PaginationResponse<ListClassroomResponse>> GetAll(ClassroomSearchRequest request);
        Task<BaseResponse<InstitutionResponse>> GetById(Guid id);
    }
}
