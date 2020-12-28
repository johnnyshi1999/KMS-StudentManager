using AutoMapper;
using StudentManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManager.AutoMapper
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentViewModel>()
                .ForMember(dest => dest.FullName, act => act.MapFrom(src =>src.FullName))
                .ForMember(dest => dest.DateOfBirth, act => act.MapFrom(src => src.DateOfBirth))
                .ForMember(dest => dest.Mathematics, act => act.MapFrom(src => src.Mathematics))
                .ForMember(dest => dest.Literatures, act => act.MapFrom(src => src.Literatures))
                .ForMember(dest => dest.English, act => act.MapFrom(src => src.English)).ReverseMap();
        }
    }
}