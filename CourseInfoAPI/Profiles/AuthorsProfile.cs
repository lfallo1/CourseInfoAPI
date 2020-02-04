using System.Collections.Generic;
using AutoMapper;
using CourseInfoAPI.Entities;
using CourseInfoAPI.Helpers;
using CourseInfoAPI.Models;

namespace CourseInfoAPI.Profiles
{
    public class AuthorsProfile : Profile
    {
        public AuthorsProfile()
        {
            CreateMap<Author, AuthorDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.GetCurrentAge())
                );

            CreateMap<AuthorCreateDto, Author>();
        }
    }
}
