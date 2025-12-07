using AutoMapper;
using Sicma.DTO.Request.Institution;
using Sicma.Entities;

namespace Sicma.Service.Mappers
{
    public  class InstitutionMap:Profile
    {
        public InstitutionMap()
        {
            CreateMap<InstitutionRequest, Institution>();
        }
    }
}
