﻿using System.Net.Http.Headers;
using System.Web.Http;

namespace TaskFlamingo
{
    public static class WebApiConfig
    {
        public static void Register( HttpConfiguration config ) {

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "Complete",
                routeTemplate: "api/{controller}/{id}/Complete",
                defaults: new { controller = "Task", action = "Complete"}
            );
            config.Routes.MapHttpRoute(
                name: "Publish",
                routeTemplate: "api/{controller}/{id}/Publish",
                defaults: new { controller = "Task", action = "Publish"}
            );

            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing( );
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html") );   
        }
    }
}
