using Autofac;
using System;

namespace DutyHours.Data
{
    public class DataIocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if(builder == null)
            {
                throw new ArgumentNullException("builder");
            }

            //TODO register types here
        }
    }
}
