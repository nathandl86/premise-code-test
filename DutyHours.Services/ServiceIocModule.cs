using Autofac;
using System;

namespace DutyHours.Services
{
    /// <summary>
    /// Autofac Module for the Services project
    /// </summary>
    public class ServiceIocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            //TODO: Register types here
        }
    }
}
