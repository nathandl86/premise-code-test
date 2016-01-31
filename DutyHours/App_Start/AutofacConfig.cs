using Autofac;
using Autofac.Integration.WebApi;
using DutyHours.Code;
using DutyHours.Data;
using DutyHours.Services;
using System.Reflection;
using System.Web.Http;

namespace DutyHours.App_Start
{
    public class AutofacConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            //configuration of autofac for web api
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);

            //types in the web project
            builder.RegisterType<Logger>()
                .AsImplementedInterfaces()
                .PropertiesAutowired();

            //Pull in modules
            builder.RegisterModule(new ServiceIocModule());
            builder.RegisterModule(new DataIocModule());

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}