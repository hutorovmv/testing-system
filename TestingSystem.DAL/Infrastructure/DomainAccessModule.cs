using System.Reflection;
using System.Configuration;
using Autofac;
using Microsoft.AspNet.Identity.EntityFramework;
using TestingSystem.DAL.Context;
using TestingSystem.DAL.Identity;
using TestingSystem.DAL.Interfaces;
using TestingSystem.DAL.Repositories;
using TestingSystem.Models.Entities;

namespace TestingSystem.DAL.Infrastructure
{
    public class DomainAccessModule :  Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TestingSystem"].ConnectionString;
            builder.RegisterType<ApplicationContext>().AsSelf().WithParameter("connectionString", connectionString).InstancePerRequest();

            builder.Register(c => new UserStore<ApplicationUser>(c.Resolve<ApplicationContext>())).As<UserStore<ApplicationUser>>().InstancePerRequest();
            builder.Register(c => new RoleStore<ApplicationRole>(c.Resolve<ApplicationContext>())).As<RoleStore<ApplicationRole>>().InstancePerRequest();

            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationRoleManager>().AsSelf().InstancePerRequest();

            Assembly dataAccessAssembly = Assembly.GetAssembly(typeof(IUnitOfWork));
            builder.RegisterAssemblyTypes(dataAccessAssembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
        }
    }
}
