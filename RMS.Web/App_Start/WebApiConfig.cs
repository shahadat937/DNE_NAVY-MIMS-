using Autofac;
using Autofac.Integration.WebApi;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Web.Http;

namespace RMS.Web.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder = RegisterDependancies.Register(builder);


            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.MapHttpAttributeRoutes();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }

}
