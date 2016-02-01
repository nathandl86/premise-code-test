using Autofac;
using DutyHours.Data.Mappers;
using System;
using System.Reflection;

namespace DutyHours.Data
{
    /// <summary>
    /// Autofac module to register the types to inject in other projects
    /// </summary>
    public class DataIocModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if(builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            //Setup the DbContext to be per request
            builder.RegisterType<DutyHoursDbContext>()
                .InstancePerRequest()
                .AsImplementedInterfaces();

            builder.RegisterType<Mapper>().AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
        }
    }
}
