using AutoMapper;
using Sicma.Common;
using Sicma.DTO.Request.UserRecord;
using Sicma.DTO.Response;
using Sicma.DTO.Response.UserRecord;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;
using Sicma.Service.Interfaces;

namespace Sicma.Service.Implementations
{
    public class UserRecordService:IUserRecordService
    {
        protected readonly IUserRecordRepository _repository;
        protected readonly IMapper _mapper;
        public UserRecordService(IUserRecordRepository repository, IMapper mapper) 
        { 
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Create(UserRecordRequest request, Guid userId)
        {
            var result = new BaseResponse();
            try
            {
                var userRecord = _mapper.Map<UserRecord>(request);
                userRecord.CreatedUserId = userId;
                userRecord.UserId = userId;

                await _repository.AddAsync(userRecord);
                result.Success = true;
                result.Message = "User record created correctly";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<BaseResponse> Delete(Guid Id)
        {
            var result = new BaseResponse();
            try
            {
                var userRecord = await _repository.FindByIdAsync(Id);
                if (userRecord == null)
                {
                    result.Success = false;
                    result.Message = "User recprd not found";
                    return result;
                }

                await _repository.DeleteAsync(Id);
                result.Success = true;
            }

            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<BaseResponse> Update(Guid id, UserRecordRequest request)
        {
            var response = new BaseResponse();

            try
            {
                var userRecord = await _repository.FindByIdAsync(id);
                if (userRecord == null)
                    throw new InvalidDataException("Institution not found");

                _mapper.Map(request, userRecord);
                await _repository.UpdateAsync();

                response.Success = true;
                response.Message = "User record updated correctly";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<PaginationResponse<ListUserRecordResponse>> GetAll(UserRecordSearchRequest request)
        {
            var response = new PaginationResponse<ListUserRecordResponse>();
            try
            {
                var result = await _repository.GetAllAsync(
                    predicate: p => p.IsActive,
                    //&&
                    //(string.IsNullOrEmpty(request.Name) || p.Name.Contains(request.Name))
                    //,
                    selector: p => _mapper.Map<ListUserRecordResponse>(p),
                    orderBy: p => p.CreatedDate,
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

        public async Task<BaseResponse<UserRecordResponse>> GetById(Guid id)
        {
            var response = new BaseResponse<UserRecordResponse>();

            try
            {
                var userRecord = await _repository.FindByIdAsync(id);
                if (userRecord == null)
                    throw new InvalidDataException("Institution not found");

                response.Data = _mapper.Map<UserRecordResponse>(userRecord);
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
