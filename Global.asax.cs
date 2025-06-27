using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using LibraryManagement.Application;
using LibraryManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LibraryManagement
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();

            // Register Web API controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register your types
            builder.RegisterType<LibraryRepository>().As<ILibraryRepository>().InstancePerRequest();
            builder.RegisterType<LibraryService>().As<ILibraryService>().InstancePerRequest();

            var container = builder.Build();

            // Set Autofac as the Web API dependency resolver
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            // Remove the second call to WebApiConfig.Register
            GlobalConfiguration.Configuration.Formatters
                .JsonFormatter.SerializerSettings.ReferenceLoopHandling
                    = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            GlobalConfiguration.Configuration.Formatters
                .JsonFormatter.SerializerSettings.NullValueHandling
                    = Newtonsoft.Json.NullValueHandling.Ignore;
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
