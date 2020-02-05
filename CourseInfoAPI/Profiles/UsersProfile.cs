using System;
using AutoMapper;
using CourseInfoAPI.Entities;
using CourseInfoAPI.Models;

namespace CourseInfoAPI.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
