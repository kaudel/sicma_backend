using Sicma.DTO.Request.User;
using Sicma.DTO.Response;
using Sicma.Entities;
using Sicma.Repositorys.Interfaces;
using Sicma.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sicma.Service.Implementations
{
    public class UserService: IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repo)
        {
            repository = repo;
        }

        public async Task<BaseResponse> Register(UserRequest request)
        {
            var response = new BaseResponse();

            try
            {
                User user = new User()
                {
                    Email = request.Email,
                    FullName = request.FullName,
                    Institution = request.Institution,
                    Nickname = request.Nickname,
                    UserTypeId = request.UserTypeId,
                };
                await repository.AddAsync(user);
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

    }
}
