using AutoMapper;
using Sicma.Common;
using Sicma.DTO.Request.OperationConfig;
using Sicma.DTO.Response;
using Sicma.DTO.Response.OperationConfig;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;
using Sicma.Service.Interfaces;

namespace Sicma.Service.Implementations
{
    public class OperationConfigService: IOperationConfigService
    {
        private readonly IOperationConfigRepository _operationConfigRepository;
        private readonly IMapper _mapper;

        public OperationConfigService(IOperationConfigRepository repository, IMapper mapper)
        {
            _operationConfigRepository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Create(OperationConfigRequest request)
        {
            var result = new BaseResponse();

            try
            {
                var operationConfig = _mapper.Map<OperationConfig>(request);
                operationConfig.CreatedUserId = "fcb8ed60-fd02-4ead-8012-efe16b109bb2";
                await _operationConfigRepository.AddAsync(operationConfig);
                result.Success = true;
                result.Message = "OperationConfig created correctly";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<BaseResponse> Delete(string operationConfigId)
        {
            var result = new BaseResponse();

            try
            {
                var operationConfig = await _operationConfigRepository.FindByIdAsync(operationConfigId);
                if (operationConfig == null)
                {
                    result.Success = false;
                    result.Message = "OperationConfig not found";
                    return result;
                }
                await _operationConfigRepository.DeleteAsync(operationConfigId);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success= false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<PaginationResponse<ListOperationConfigResponse>> GetAll(OperationConfigSearchRequest request)
        {
            var response = new PaginationResponse<ListOperationConfigResponse>();
            try
            {
                var result = await _operationConfigRepository.GetAllAsync(
                    predicate: p => p.IsActive
                    &&
                    (string.IsNullOrEmpty(request.OperationName) || p.OperationName.Contains(request.OperationName))
                    ,
                    selector: p => _mapper.Map<ListOperationConfigResponse>(p),
                    orderBy: p => p.OperationName,
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
                response.Success= false;
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task<BaseResponse<OperationConfigResponse>> GetById(string id)
        {
            var response = new BaseResponse<OperationConfigResponse>();

            try
            {
                var operationConfig = await _operationConfigRepository.FindByIdAsync(id);
                if (operationConfig == null)
                    throw new InvalidDataException("Institution not found");

                response.Data = _mapper.Map<OperationConfigResponse>(operationConfig);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse> Update(string id, OperationConfigRequest request)
        {
            var response = new BaseResponse();

            try
            {
                var operationConfig = await _operationConfigRepository.FindByIdAsync(id);
                if (operationConfig == null)
                    throw new InvalidDataException("OperationConfig not found");

                _mapper.Map(request, operationConfig);
                await _operationConfigRepository.UpdateAsync();

                response.Success = true;
                response.Message = "OperationConfig updated correctly";

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
