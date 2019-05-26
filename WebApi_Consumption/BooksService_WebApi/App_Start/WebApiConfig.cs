using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BooksService_WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // https://stackoverflow.com/questions/7397207/json-net-error-self-referencing-loop-detected-for-type
            // better to add [JsonIgnore] (for JSON) and [IgnoreDataMember] (for XML) as 
            // attributes on Navigation Properties in the classes themselves.
            // config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

        }
    }
}
