using AutoMapper;
using Sicma.DTO.Request.Institution;
using Sicma.DTO.Response.Institutions;
using Sicma.Entities;

namespace Sicma.Service.Mappers
{
    public  class InstitutionMap:Profile
    {
        public InstitutionMap()
        {
            CreateMap<InstitutionRequest, Institution>();
            CreateMap<Institution, ListInstitutionsResponse>();
            CreateMap<Institution, InstitutionResponse>();
        }
    }
}
