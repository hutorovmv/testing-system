using System.Reflection;
using Autofac;
using TestingSystem.BLL.Interfaces;

namespace TestingSystem.BLL.Infrastructure
{
    public class BusinessLogicModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Assembly businessLogicAssembly = Assembly.GetAssembly(typeof(IUserManagementService));
            builder.RegisterAssemblyTypes(businessLogicAssembly).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();
        }
    }
}
