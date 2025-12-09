using AutoMapper;
using Sicma.DTO.Request.User;
using Sicma.DTO.Response.Users;
using Sicma.Entities;

namespace Sicma.Service.Mappers
{
    public class UserMap: Profile
    {
        public UserMap() 
        {
            CreateMap<UserRequest, AppUser>();
            CreateMap<AppUser, ListUsersResponse>();
            CreateMap<AppUser, UserResponse>();
            CreateMap<AppUser, UserData>();
            CreateMap<AppUser, UserLoginResponse>();
        }
    }
}
