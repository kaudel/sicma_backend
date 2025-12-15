using AutoMapper;
using Sicma.Common;
using Sicma.DTO.Request.Classroom;
using Sicma.DTO.Response;
using Sicma.DTO.Response.Classroom;
using Sicma.DTO.Response.Institutions;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;
using Sicma.Service.Interfaces;

namespace Sicma.Service.Implementations
{
    public class ClassroomService:IClassroomService
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly IMapper _mapper;

        public ClassroomService(IClassroomRepository reposiory, IMapper mapper)
        {
            _classroomRepository = reposiory;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Create(ClassroomRequest request, Guid userId)
        {
            var result = new BaseResponse();
            try
            {
                var clasrroom = _mapper.Map<Classroom>(request);
                clasrroom.CreatedUserId = userId;

                await _classroomRepository.AddAsync(clasrroom);
                result.Success = true;
                result.Message = "Classroom created correctly";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<BaseResponse> Delete(Guid classroomId)
        {
            var result = new BaseResponse();
            try
            {
                var classroom = await _classroomRepository.FindByIdAsync(classroomId);
                if (classroom == null)
                {
                    result.Success = false;
                    result.Message = "Classroom not found";
                    return result;
                }

                await _classroomRepository.DeleteAsync(classroomId);
                result.Success = true;
            }

            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<BaseResponse> Update(Guid id, ClassroomRequest request)
        {
            var response = new BaseResponse();

            try
            {
                var classroom = await _classroomRepository.FindByIdAsync(id);
                if (classroom == null)
                    throw new InvalidDataException("Classroom not found");

                _mapper.Map(request, classroom);
                await _classroomRepository.UpdateAsync();

                response.Success = true;
                response.Message = "Classroom updated correctly";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<PaginationResponse<ListClassroomResponse>> GetAll(ClassroomSearchRequest request)
        {
            var response = new PaginationResponse<ListClassroomResponse>();
            try
            {
                var result = await _classroomRepository.GetAllAsync(
                    predicate: p => p.IsActive
                    &&
                    (string.IsNullOrEmpty(request.Name) || p.Name.Contains(request.Name))
                    ,
                    selector: p => _mapper.Map<ListClassroomResponse>(p),
                    orderBy: p => p.Name,
                    page: request.Page,
                    rows: request.Rows
                    );

                response.Data = result.Collection;
                response.Success = true;
                response.TotalRows = result.TotalRecords;
                response.TotalPages = Helpers.CalculatePageNumber(result.TotalRecords, request.Rows);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<InstitutionResponse>> GetById(Guid id)
        {
            var response = new BaseResponse<InstitutionResponse>();

            try
            {
                var classroom = await _classroomRepository.FindByIdAsync(id);
                if (classroom == null)
                    throw new InvalidDataException("Classroom not found");

                response.Data = _mapper.Map<InstitutionResponse>(classroom);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
