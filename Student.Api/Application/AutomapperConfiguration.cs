using AutoMapper;
using Students.Api.Application.Models;
using Students.Api.Persistence;

namespace Students.Api.Application
{
    public class AutomapperConfiguration : Profile
    {
        public AutomapperConfiguration()
        {
            CreateMap<Student, StudentModel>().ReverseMap();
        }
    }
}
