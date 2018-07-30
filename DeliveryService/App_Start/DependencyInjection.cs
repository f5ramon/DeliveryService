using System.Reflection;
using System.Web.Http;
using DeliveryService.Data;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.WebApi;

namespace DeliveryService
{
    public class DependencyInjection
    {
        public Container Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Register your types, for instance:
            container.Register<UnitOfWork, UnitOfWork>(Lifestyle.Scoped);
            //container.Register<EmployeesBusiness, EmployeesBusiness>(Lifestyle.Scoped);
            //container.Register<JobsBusiness, JobsBusiness>(Lifestyle.Scoped);
            //container.Register<ShiftsBusiness, ShiftsBusiness>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            return container;
        }

    }
}