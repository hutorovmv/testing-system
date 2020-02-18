using AutoMapper;
using TestingSystem.BLL.DTO;
using TestingSystem.Models.Entities;

namespace TestingSystem.BLL.Infrastructure
{
    public class DomainModelToDtoProfile : Profile
    {
        public DomainModelToDtoProfile()
        {
            CreateMap<ApplicationUser, UserDTO>()
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.IsEmailConfirmed, opt => opt.MapFrom(src => src.EmailConfirmed));
            CreateMap<UserProfile, UserDTO>();

            CreateMap<Test, TestDTO>();
            CreateMap<Question, QuestionDTO>();
            CreateMap<Answer, AnswerDTO>();
            CreateMap<TestResult, TestResultDTO>();
            CreateMap<TestAnswer, TestAnswerDTO>();
        }
    }
}
