using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;

namespace GenericWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
          //  config.SuppressDefaultHostAuthentication();
          //  config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

          //  var corsAttr = new EnableCorsAttribute("http://10.34.1.80, http://localhost:24857, http://localhost:13001, http://10.34.2.162", "Origin, Content-Type, Accept", "POST, GET, OPTIONS");
          //  config.EnableCors(corsAttr);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.Routes.MapHttpRoute(
               name: "DefaultApi",
               routeTemplate: "api/{controller}/{proc}/{ids}",
               defaults: new { proc = RouteParameter.Optional, ids = RouteParameter.Optional }
           );

        }
    }
}
