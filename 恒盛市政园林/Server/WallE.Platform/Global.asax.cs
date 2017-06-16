using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using WallE.Data.MySql;
using WallE.Platform.Support;
using WebApiContrib.Formatting.Jsonp;

namespace WallE.Platform
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            M.Init();
            MgntDbManager.CreateManagementDb();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.AddJsonpFormatter();
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
        }
    }
}