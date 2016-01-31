using Autofac;
using System;
using System.Reflection;

namespace DutyHours.Services
{
    /// <summary>
    /// Autofac Module for the Services project
    /// </summary>
    public class ServiceIocModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
        }
    }
}
