using AutoMapper;
using CourseInfoAPI.Entities;
using CourseInfoAPI.Models;

namespace CourseInfoAPI.Profiles
{
    public class CoursesProfile : Profile
    {
        public CoursesProfile()
        {
            CreateMap<Course, CourseDto>();
        }
    }
}
