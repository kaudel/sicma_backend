using AutoMapper;
using Sicma.DTO.Request.PracticeConfig;
using Sicma.DTO.Response.PracticeConfig;
using Sicma.Entities;

namespace Sicma.Service.Mappers
{
    public class PracticeConfigMap:Profile
    {
        public PracticeConfigMap() 
        {
            CreateMap<PracticeConfigRequest, PracticeConfig>();
            CreateMap<PracticeConfig, ListPracticeConfigResponse>();
            CreateMap<PracticeConfig, PracticeConfigResponse>();
        }
        
    }
}
