using System;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using WebApiNew;

namespace MyDbWebApi
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.IncludeErrorDetailPolicy= IncludeErrorDetailPolicy.Always;
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Access-Control-Allow-Headers,Pragma,Token, Authorization ,Origin,Accept, X-Requested-With, Content-Type,enctype,Cache-Control, Pragma,Contect-Transfer-Encoding,Access-Control-Request-Method, Access-Control-Request-Headers");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
        }
    }
}
