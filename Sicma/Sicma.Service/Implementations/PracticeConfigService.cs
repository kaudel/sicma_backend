using AutoMapper;
using Sicma.Common;
using Sicma.DTO.Request.PracticeConfig;
using Sicma.DTO.Response;
using Sicma.DTO.Response.PracticeConfig;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;
using Sicma.Service.Interfaces;

namespace Sicma.Service.Implementations
{
    public class PracticeConfigService:IPracticeConfigService
    {
        private readonly IPracticeConfigRepository _pConfigrepository;
        private readonly IMapper _mapper;

        public PracticeConfigService(IPracticeConfigRepository practiceConfigRepo, IMapper mapper)
        {
            _pConfigrepository = practiceConfigRepo;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Create(PracticeConfigRequest request, string userId)
        {
            var result = new BaseResponse();
            try
            {
                var practiceConfig = _mapper.Map<PracticeConfig>(request);
                practiceConfig.CreatedUserId = userId;

                await _pConfigrepository.AddAsync(practiceConfig);
                result.Success = true;
                result.Message = "PracticeConfig created correctly";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<BaseResponse> Delete(string practiceConfigId)
        {
            var result = new BaseResponse();
            try
            {
                var institution = await _pConfigrepository.FindByIdAsync(practiceConfigId);
                if (institution == null)
                {
                    result.Success = false;
                    result.Message = "Practice config not found";
                    return result;
                }

                await _pConfigrepository.DeleteAsync(practiceConfigId);
                result.Success = true;
            }

            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<BaseResponse> Update(string id, PracticeConfigRequest request)
        {
            var response = new BaseResponse();

            try
            {
                var practiceConfig = await _pConfigrepository.FindByIdAsync(id);
                if (practiceConfig == null)
                    throw new InvalidDataException("Practice config not found");

                _mapper.Map(request, practiceConfig);
                await _pConfigrepository.UpdateAsync();

                response.Success = true;
                response.Message = "Practice config updated correctly";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<PaginationResponse<ListPracticeConfigResponse>> GetAll(PracticeConfigSearchRequest request)
        {
            var response = new PaginationResponse<ListPracticeConfigResponse>();
            try
            {
                var result = await _pConfigrepository.GetAllAsync(
                    predicate: p => p.IsActive
                    &&
                    (string.IsNullOrEmpty(request.Name) || p.Name.Contains(request.Name))
                    ,
                    selector: p => _mapper.Map<ListPracticeConfigResponse>(p),
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

        public async Task<BaseResponse<PracticeConfigResponse>> GetById(string id)
        {
            var response = new BaseResponse<PracticeConfigResponse>();

            try
            {
                var institution = await _pConfigrepository.FindByIdAsync(id);
                if (institution == null)
                    throw new InvalidDataException("Institution not found");

                response.Data = _mapper.Map<PracticeConfigResponse>(institution);
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
