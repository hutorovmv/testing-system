using System;
using AutoMapper;
using TestingSystem.Web.Areas.Admin.Data;
using TestingSystem.Web.Areas.User.Data;
using TestingSystem.Web.Models;
using TestingSystem.BLL.DTO;

namespace TestingSystem.Web.Infrastructure
{
    public class ViewModelToDtoProfile : Profile
    {
        public ViewModelToDtoProfile()
        {
            CreateMap<LoginModel, UserDTO>();
            CreateMap<RegisterModel, UserDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "user"));
            CreateMap<ProfileSettingsModel, UserDTO>()
                .ForMember(dest => dest.ProfilePhoto, opt => opt.Ignore());

            CreateMap<TestEditModel, TestDTO>()
                .ForMember(dest => dest.TimeRequired, opt =>
                {
                    opt.PreCondition(src => src.TimeRequired.HasValue);
                    opt.MapFrom(src => src.TimeRequired.Value.TotalSeconds);
                });
            CreateMap<QuestionEditModel, QuestionDTO>();
            CreateMap<AnswerEditModel, AnswerDTO>();
        }
    }
}