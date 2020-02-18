using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using TestingSystem.BLL.Infrastructure;
using TestingSystem.DAL.Infrastructure;

namespace TestingSystem.Web.App_Start
{
    public class AutofacRegistration
    {
        public static IContainer BuildContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            Assembly webAppAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterControllers(webAppAssembly);
            builder.Register(c => HttpContext.Current.GetOwinContext().Authentication).As<IAuthenticationManager>();

            builder.Register(c => AutomapperConfiguration.Configure()).As<IMapper>().SingleInstance();

            builder.RegisterModule(new DomainAccessModule());
            builder.RegisterModule(new BusinessLogicModule());

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            return container;
        }
    }
}