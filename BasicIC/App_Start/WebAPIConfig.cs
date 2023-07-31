    using Common;
    using BasicIC.CustomAttributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Configuration;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using System.Web.Routing;

    namespace BasicIC
    {
        public static class WebApiConfig
        {
            public static void Register(HttpConfiguration config)
            {
                config.MapHttpAttributeRoutes();

                config.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{action}/{id}",
                    defaults: new { id = RouteParameter.Optional }

                );

                config.Filters.Add(new ValidateModelAttribute());
            }
        }
    }
