using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using RMS.BLL.IManager;
using RMS.BLL.Manager;

namespace RMS.Web.App_Start
{
    public class DipendencyConfig
    {
        private static IContainer _container;

        public static void SetupContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder = RegisterDependancies.Register(builder);

            builder.RegisterFilterProvider();
            _container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }

        private static T Resolve<T>()
        {
            if (_container == null)
                SetupContainer();

            return _container.Resolve<T>();
        }
    }
}