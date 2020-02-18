using System;
using AutoMapper;
using TestingSystem.Web.Areas.Admin.Data;
using TestingSystem.Web.Areas.User.Data;
using TestingSystem.Web.Models;
using TestingSystem.BLL.DTO;

namespace TestingSystem.Web.Infrastructure
{
    public class DtoToViewModelProfile : Profile
    {
        public DtoToViewModelProfile()
        {
            CreateMap<UserDTO, UserProfileModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<UserDTO, UserCardModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => src.Role == "admin")); ;
            CreateMap<UserDTO, UserTableModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
            CreateMap<UserDTO, ProfileSettingsModel>();

            CreateMap<TestDTO, TestCardModel>()
                .ForMember(dest => dest.TimeRequired, opt =>
                {
                    opt.PreCondition(src => src.TimeRequired.HasValue);
                    opt.MapFrom(src => TimeSpan.FromSeconds(src.TimeRequired.Value));
                });
            CreateMap<TestDTO, TestTableModel>()
                .ForMember(dest => dest.TimeRequired, opt =>
                {
                    opt.PreCondition(src => src.TimeRequired.HasValue);
                    opt.MapFrom(src => TimeSpan.FromSeconds(src.TimeRequired.Value));
                });
            CreateMap<TestDTO, TestEditModel>()
                .ForMember(dest => dest.TimeRequired, opt =>
                {
                    opt.PreCondition(src => src.TimeRequired.HasValue);
                    opt.MapFrom(src => TimeSpan.FromSeconds(src.TimeRequired.Value));
                });
            CreateMap<QuestionDTO, QuestionTableModel>();
            CreateMap<QuestionDTO, QuestionEditModel>();
            CreateMap<AnswerDTO, AnswerTableModel>();
            CreateMap<AnswerDTO, AnswerEditModel>();
            CreateMap<TestResultDTO, TestResultTableModel>();
        }
    }
}