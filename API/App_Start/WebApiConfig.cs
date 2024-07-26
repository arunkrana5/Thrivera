using System.Web.Http;
using System.Web.Http.Cors;
using DataBooster.DbWebApi;

namespace MyDbWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            EnableCors(config);

            // Web API routes

            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                      name: "DownloadSalarySlip",
                      routeTemplate: "DownloadSalarySlip/{Month}/{EMPCode}",
                      defaults: new { controller = "DbWebApi", action = "DownloadSalarySlip"},
                      constraints: new { ext = @"|json|bson|xml|csv|xlsx|jsonp|razor" }
                  );


            config.Routes.MapHttpRoute(
              name: "DbWebApi",
              routeTemplate: "{sp}/{ext}",
              defaults: new { controller = "DbWebApi", action = "DynExecute", ext = RouteParameter.Optional },
              constraints: new { ext = @"|json|bson|xml|csv|xlsx|jsonp|razor" }
          );

            config.Routes.MapHttpRoute(
                      name: "RegisterUser",
                      routeTemplate: "{sp}/{other}/{ext}",
                      defaults: new { controller = "DbWebApi", action = "RegisterUser", ext = RouteParameter.Optional },
                      constraints: new { ext = @"|json|bson|xml|csv|xlsx|jsonp|razor" }
                  );

            //config.Routes.MapHttpRoute(
            //         name: "PerformOperationBeforeSPCall",
            //         routeTemplate: "{sp}/{other}/{ext}",
            //         defaults: new { controller = "DbWebApi", action = "PerformOperationBeforeSPCall", ext = RouteParameter.Optional },
            //         constraints: new { ext = @"|json|bson|xml|csv|xlsx|jsonp|razor" }
            //     );
            //config.Routes.MapHttpRoute(
            //      name: "DataSaveWithFileupload",
            //      routeTemplate: "{sp}/{Fileupload}/{ext}",
            //       defaults: new { controller = "DbWebApi", action = "DataSaveWithFileupload", ext = RouteParameter.Optional },
            //      constraints: new { ext = @"|json|bson|xml|csv|xlsx|jsonp|razor" }
            //  );
            config.Routes.MapHttpRoute(
                     name: "Login",
                     routeTemplate: "{sp}/{other}/{auth}/{ext}",
                      defaults: new { controller = "DbWebApi", action = "Login", ext = RouteParameter.Optional },
                     constraints: new { ext = @"|json|bson|xml|csv|xlsx|jsonp|razor" }
                 );
          
            config.Routes.MapHttpRoute(
                    name: "SendMail",
                    routeTemplate: "{email}/{body}/{subj}/{other}/{user}/{emailsend}/{mailsend}",
                     defaults: new { controller = "DbWebApi", action = "SendMail", ext = RouteParameter.Optional },
                    constraints: new { ext = @"|json|bson|xml|csv|xlsx|jsonp|razor" }
                );

            config.Routes.MapHttpRoute(
                name: "VerifyEmailId",
                routeTemplate: "{sp}/{other}/{auth}/{verify}/{email}/{ext}",
                defaults: new { controller = "DbWebApi", action = "VerifyEmailId", ext = RouteParameter.Optional },
                constraints: new { ext = @"|json|bson|xml|csv|xlsx|jsonp|razor" }
            );
            config.Routes.MapHttpRoute(
                   name: "CommonFunction",
                   routeTemplate: "{sp}/{other}/{auth}/{check}/{function}/{change}/{common}/{ext}",
                     defaults: new { controller = "DbWebApi", action = "CommonFunction", ext = RouteParameter.Optional },
                    constraints: new { ext = @"|json|bson|xml|csv|xlsx|jsonp|razor" }
               );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
              routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //config.Routes.MapHttpRoute(
            //    name: "Default",
            //    routeTemplate: "api/{controller}/{action}"
            //);

            //	config.RegisterDbWebApi();
#if DEBUG
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
#endif
            //	DbWebApiOptions.DefaultPropertyNamingConvention = PropertyNamingConvention.PascalCase;

            DbWebApiAuthorizeAttribute.RegisterWebApiAuthorization<MyDbWebApiAuthorization>();
        }

        private static void EnableCors(HttpConfiguration config)
        {
            if (string.IsNullOrEmpty(ConfigHelper.CorsOrigins)) return;
            var cors = new EnableCorsAttribute(ConfigHelper.CorsOrigins, "*", "*")
            {
                SupportsCredentials = ConfigHelper.SupportsCredentials
            };

            if (ConfigHelper.PreflightMaxAge > 0L)
                cors.PreflightMaxAge = ConfigHelper.PreflightMaxAge;

            config.EnableCors(cors);
        }
    }
}
