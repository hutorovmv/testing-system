using AutoMapper;
using TestingSystem.Web.Infrastructure;
using TestingSystem.BLL.Infrastructure;

namespace TestingSystem.Web.App_Start
{
    public class AutomapperConfiguration
    {
        public static IMapper Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainModelToDtoProfile>();
                cfg.AddProfile<DtoToDomainModelProfile>();
                cfg.AddProfile<DtoToViewModelProfile>();
                cfg.AddProfile<ViewModelToDtoProfile>();
            });

            return config.CreateMapper();
        }
    }
}