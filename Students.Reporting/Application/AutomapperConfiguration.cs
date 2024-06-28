using AutoMapper;
using Students.Reporting.Application.Models;
using Students.Reporting.Persistence;

namespace Students.Reporting.Application
{
    public class AutomapperConfiguration : Profile
    {
        public AutomapperConfiguration()
        {
            CreateMap<StudentReport, StudentReportModel>().ReverseMap();
        }
    }
}
