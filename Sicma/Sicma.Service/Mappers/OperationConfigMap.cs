using AutoMapper;
using Sicma.DTO.Request.OperationConfig;
using Sicma.DTO.Response.OperationConfig;
using Sicma.Entities;

namespace Sicma.Service.Mappers
{
    public class OperationConfigMap:Profile
    {
        public OperationConfigMap()
        {
            CreateMap<OperationConfigRequest, OperationConfig>();
            CreateMap<OperationConfig, ListOperationConfigResponse>();
            CreateMap<OperationConfig, OperationConfigResponse>();
        }
    }
}
