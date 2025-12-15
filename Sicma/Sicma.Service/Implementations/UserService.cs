using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Sicma.Common;
using Sicma.DTO.Request.User;
using Sicma.DTO.Response;
using Sicma.DTO.Response.Users;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;
using Sicma.Service.Interfaces;

namespace Sicma.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;
        private readonly UserManager<AppUser> _userManager;

        public UserService(IUserRepository repo, IMapper mapper, IUnitOfWork uow, 
            UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _repository = repo;
            _mapper = mapper;
            _userManager = userManager;
            _uow = uow;
        }

        public async Task<BaseResponse> Register(UserRequest request)
        {
            var response = new BaseResponse();

            try
            {
                await _uow.BeginTransactionAsync();
                
                if (!_repository.IsUnique(request.UserName))
                {
                    response.Success = false;
                    response.Message = "UserName is already used";
                    return response;
                }

                var user = _mapper.Map<AppUser>(request);
                user.CreatedUserId = Guid.Parse("fcb8ed60-fd02-4ead-8012-efe16b109bb2");

                IdentityResult result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    response.Success = false;
                    response.Message = result.Errors.FirstOrDefault().Description;
                    return response;
                }
                await _userManager.AddToRoleAsync(user, request.UserRole);

                await _uow.CommitAsync();

                response.Message = "User created successfully";
                response.Success = true;
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
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
                AppUser user = new()
                {
                    UserName = request.FullName,
                    Email = request.Email
                };

                var result = await _repository.GetAllAsync(
                    predicate: p => p.IsActive
                    &&
                    //(string.IsNullOrEmpty(request.Institution) || p.Institution.Contains(request.Institution)) &&
                    (string.IsNullOrEmpty(request.FullName) || p.UserName!.Contains(request.FullName))
                    ,
                    selector: p => new ListUsersResponse
                    {
                        Id = p.Id,
                        UserName = p.UserName,
                        FullName = p.FullName,
                        Email = p.Email,
                        //Institution = p.Institution,
                        //UserType = p.UserTypeId
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
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<UserResponse>> GetById(Guid id)
        {
            var response = new BaseResponse<UserResponse>();

            try
            {
                var user = await _repository.FindByIdAsync(id);
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "User not found";
                    return response;
                }

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

        public async Task<BaseResponse> Update(Guid id, UserRequest request)
        {
            var response = new BaseResponse();

            try
            {
                var user = await _repository.FindByIdAsync(id);

                if (user == null) throw new InvalidDataException("User not exists");

                user.FullName = request.FullName;

                await _repository.UpdateAsync();

                response.Success = true;
                response.Message = "User updated successfully";

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse> Delete(Guid id)
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

        public async Task<BaseResponse<UserAutenticateResponse>> Authenticate(UserLoginRequest userLogin)
        {
            var response = new BaseResponse<UserAutenticateResponse>();
            var user = _repository.GetUserByUserName(userLogin.UserName);

            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
                return response;
            }

            bool isValid = await _userManager.CheckPasswordAsync(user, userLogin.Password);

            if (user == null || !isValid)
            {
                response.Success = false;
                response.Message = "Username o password is incorrect";
                return response;
            }

            UserAutenticateResponse userAuthResponse = new UserAutenticateResponse()
            {
                Id = user.Id,
                Email = user.Email!,
                UserName = user.UserName!
            };

            response.Success = true;
            response.Message = "User authenticated";
            response.Data = userAuthResponse;

            return response;
        }

    }
}
