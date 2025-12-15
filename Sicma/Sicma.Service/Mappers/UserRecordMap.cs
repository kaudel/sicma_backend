using AutoMapper;
using Sicma.DTO.Request.UserRecord;
using Sicma.DTO.Response.UserRecord;
using Sicma.Entities;

namespace Sicma.Service.Mappers
{
    public class UserRecordMap:Profile
    {
        public UserRecordMap()
        {
            CreateMap<UserRecordRequest, UserRecord>();
            CreateMap<UserRecord, ListUserRecordResponse>();
            CreateMap<UserRecord, UserRecordResponse>();
        }
    }
}
