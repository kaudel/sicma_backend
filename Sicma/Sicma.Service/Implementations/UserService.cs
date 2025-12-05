using AutoMapper;
using Sicma.Common;
using Sicma.DTO.Request.User;
using Sicma.DTO.Response;
using Sicma.DTO.Response.Users;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;
using Sicma.Service.Interfaces;

namespace Sicma.Service.Implementations
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Register(UserRequest request)
        {
            var response = new BaseResponse();

            try
            {
                var user = _mapper.Map<User>(request);

                await _repository.AddAsync(user);
                response.Message = "User created successfully";
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
            
        }

        public async Task<PaginationResponse<ListUsersResponse>> GetAll(UserSearchRequest request)
        {
            var response = new PaginationResponse<ListUsersResponse>();

            try
            {
                User user = new()
                {
                    FullName = request.FullName,
                    Institution = request.Institution,
                    Email = request.Email
                };

                var result = await _repository.GetAllAsync(
                    predicate: p => p.IsActive 
                    &&
                    (string.IsNullOrEmpty(request.Institution) || p.Institution.Contains(request.Institution)) &&
                    (string.IsNullOrEmpty(request.FullName) || p.FullName.Contains(request.FullName))
                    ,
                    selector: p => new ListUsersResponse 
                    { 
                        Id = p.Id,
                        Nickname = p.Nickname,
                        FullName = p.FullName,
                        Email = p.Email,
                        Institution = p.Institution,
                        UserType = p.UserTypeId
                    },
                    //_mapper.Map<ListUsersResponse>(p),
                    orderBy: p => p.FullName,
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
                response.Message= ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<UserResponse>> GetById(string id)
        {
            var response = new BaseResponse<UserResponse>();

            try
            {
                var user = await _repository.FindByIdAsync(id);
                if (user == null) throw new InvalidDataException("User not found");

                response.Data = _mapper.Map<UserResponse>(user);
                response.Success = true;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse> Update(string id, UserRequest request)
        {
            var response = new BaseResponse();

            try
            {
                var user = await _repository.FindByIdAsync(id);

                if (user == null) throw new InvalidDataException("User not exists");

                //do the mapping directly
                _mapper.Map(request, user);
                await _repository.UpdateAsync();

                response.Success = true;
                response.Message = "User updated successfully";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message= ex.Message;
            }
            
            return response;
        }

        public async Task<BaseResponse> Delete(string id)
        {
            var response = new BaseResponse();

            try
            {
                var user = await _repository.FindByIdAsync(id);
                if (user == null) throw new InvalidDataException("User not found");

                await _repository.DeleteAsync(id);
                response.Success = true;
                response.Message = "User deleted successfully";

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
