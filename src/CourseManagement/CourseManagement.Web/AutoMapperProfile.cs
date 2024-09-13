using AutoMapper;
using CourseManagement.Application.DTOs;
using CourseManagement.Domain.Entities;
using CourseManagement.Infrastructure.Membership;
using CourseManagement.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace CourseManagement.Web
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CourseVM, Course>().ReverseMap();
            CreateMap<CourseEnrollMentVM, CourseEnrollmentDto>().ReverseMap();

            CreateMap<ApplicationUser, AccountVM>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName)) // Assuming IdentityUser has FirstName property
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName ?? string.Empty))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.Id));

        }
    }
}
