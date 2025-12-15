using AutoMapper;
using Sicma.DTO.Request.Classroom;
using Sicma.DTO.Response.Classroom;
using Sicma.Entities;

namespace Sicma.Service.Mappers
{
    public class ClassroomMap:Profile
    {
        public ClassroomMap() 
        {
            CreateMap<ClassroomRequest, Classroom>();
            CreateMap<Classroom, ListClassroomResponse>();
            CreateMap<Classroom, ClassroomResponse>();
        }   
        
    }
}
