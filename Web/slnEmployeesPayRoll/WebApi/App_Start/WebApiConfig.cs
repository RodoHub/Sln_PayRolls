using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApi_PayRoll
{
    /// <summary>
    /// Web Api Config class
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register method
        /// </summary>
        /// <param name="config">Http Configuration</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            //Replies all content of controllers as "serializers" (Format JSON)
            var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            formatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

        }
    }
}
