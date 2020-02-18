using AutoMapper;
using TestingSystem.BLL.DTO;
using TestingSystem.Models.Entities;

namespace TestingSystem.BLL.Infrastructure
{
    public class DtoToDomainModelProfile : Profile
    {
        public DtoToDomainModelProfile()
        {
            CreateMap<UserDTO, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.IsEmailConfirmed));
            CreateMap<UserDTO, UserProfile>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.BirthDate, opt => opt.Condition(src => src.BirthDate != null))
                .ForMember(dest => dest.ProfilePhoto, opt => opt.Condition(src => src.ProfilePhoto != null));

            CreateMap<TestDTO, Test>();
            CreateMap<QuestionDTO, Question>();
            CreateMap<AnswerDTO, Answer>();
            CreateMap<TestResultDTO, TestResult>();
            CreateMap<TestAnswerDTO, TestAnswer>();
        }
    }
}
