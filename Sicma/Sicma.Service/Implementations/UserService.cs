using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sicma.Common;
using Sicma.DTO.Request.User;
using Sicma.DTO.Response;
using Sicma.DTO.Response.Users;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;
using Sicma.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sicma.Service.Implementations
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string _secretKey;
        private readonly IConfiguration _config;

        public UserService(IUserRepository repo, IMapper mapper, IConfiguration config, UserManager<AppUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _repository = repo;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<BaseResponse> Register(UserRequest request)
        {
            var response = new BaseResponse();

            try
            {
                if (!_repository.IsUnique(request.UserName))
                {
                    response.Success = false;
                    response.Message = "UserName is already used";
                    return response;
                }

                var user = _mapper.Map<AppUser>(request);
                user.CreatedUserId = "fcb8ed60-fd02-4ead-8012-efe16b109bb2";

                IdentityResult result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    response.Success = false;
                    response.Message = result.Errors.FirstOrDefault().Description;
                    return response;
                }
                await _userManager.AddToRoleAsync(user, request.UserRole);

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
                AppUser user = new()
                {
                    UserName = request.FullName,
                    
                    //Institution = request.Institution,
                    Email = request.Email
                };

                var result = await _repository.GetAllAsync(
                    predicate: p => p.IsActive 
                    &&
                    //(string.IsNullOrEmpty(request.Institution) || p.Institution.Contains(request.Institution)) &&
                    (string.IsNullOrEmpty(request.FullName) || p.UserName.Contains(request.FullName))
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

        public async Task<BaseResponse> Update(string id, UserRequest request)
        {
            var response = new BaseResponse();

            try
            {
                var user = await _repository.FindByIdAsync(id);

                if (user == null) throw new InvalidDataException("User not exists");

                //_mapper.Map(request, user);
                //At this moment only the full name is a field editable
                user.FullName = request.FullName;

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

        public async Task<BaseResponse<UserLoginResponse>> Login(UserLoginRequest userLogin)
        {
            var response = new BaseResponse<UserLoginResponse>();
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
                //return new UserLoginResponse()
                //{
                //    Token = "",
                //    User = null
                //};
            }

            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();

            string secretKey = _config["APISettings:secretKey"];
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                ]),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            UserLoginResponse userResponse = new UserLoginResponse()
            {
                Token = tokenHandler.WriteToken(token),
                User = _mapper.Map<UserData>(user),
            };

            response.Success = true;
            response.Message = "User authenticated";
            response.Data = userResponse;

            return response;
        }

    }
}
