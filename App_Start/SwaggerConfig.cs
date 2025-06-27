using System.Web.Http;
using WebActivatorEx;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(LibraryManagement.App_Start.SwaggerConfig), "Register")]

namespace LibraryManagement.App_Start
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "LibraryManagement API");
                        // You can add more configuration here if needed
                    })
                .EnableSwaggerUi();
        }
    }
}