using AutoMapper;
using Sicma.Common;
using Sicma.DTO.Request.Institution;
using Sicma.DTO.Response;
using Sicma.DTO.Response.Institutions;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;
using Sicma.Service.Interfaces;

namespace Sicma.Service.Implementations
{
    public class InstitutionService:IInstitutionService
    {
        private readonly IInstitutionRepository _institutionRepository;
        private readonly IMapper _mapper;

        public InstitutionService( IInstitutionRepository repository, IMapper mapper)
        {
            _institutionRepository = repository;
            _mapper = mapper;            
        }

        public async Task<BaseResponse> Create(InstitutionRequest request, Guid userId)
        {
            var result = new BaseResponse();
            try
            {
                var institution = _mapper.Map<Institution>(request);
                institution.CreatedUserId = userId;

                await _institutionRepository.AddAsync(institution);
                result.Success = true;
                result.Message = "Institute created correctly";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<BaseResponse> Delete(Guid institutionId)
        {
            var result = new BaseResponse();
            try
            {
                var institution = await _institutionRepository.FindByIdAsync(institutionId);
                if (institution == null)
                {
                    result.Success = false;
                    result.Message = "Institution not found";
                    return result;
                }

                await _institutionRepository.DeleteAsync(institutionId);
                result.Success = true;
            }

            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<PaginationResponse<ListInstitutionsResponse>> GetAll(InstitutionSearchRequest request)
        {
            var response = new PaginationResponse<ListInstitutionsResponse>();
            try
            {
                var result = await _institutionRepository.GetAllAsync(
                    predicate: p =>p.IsActive
                    &&
                    (string.IsNullOrEmpty(request.Name) || p.Name.Contains(request.Name))
                    ,
                    selector: p=> _mapper.Map<ListInstitutionsResponse>(p),
                    orderBy: p=> p.Name,
                    page: request.Page,
                    rows: request.Rows
                    );

                response.Data = result.Collection;
                response.Success= true;
                response.TotalRows = result.TotalRecords;
                response.TotalPages = Helpers.CalculatePageNumber(result.TotalRecords, request.Rows);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message= ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<InstitutionResponse>> GetById(Guid id)
        {
            var response = new BaseResponse<InstitutionResponse>();

            try
            {
                var institution = await _institutionRepository.FindByIdAsync(id);
                if (institution == null)
                    throw new InvalidDataException("Institution not found");

                response.Data = _mapper.Map<InstitutionResponse>(institution);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message= ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse> Update(Guid id, InstitutionRequest request)
        {
            var response = new BaseResponse();

            try
            {
                var institution = await _institutionRepository.FindByIdAsync(id);
                if (institution == null)
                    throw new InvalidDataException("Institution not found");

                _mapper.Map(request, institution);
                await _institutionRepository.UpdateAsync();

                response.Success = true;
                response.Message = "Institution updated correctly";

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
