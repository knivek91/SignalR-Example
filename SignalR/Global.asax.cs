using Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace SignalR
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

        }

        void Session_Start(Object sender, EventArgs E)
        {
            var a = this.Session.SessionID; // es lo mismo que hacer -> Context.Request.Cookies["ASP.NET_SessionId"].Value

            Models.Session.Instancia.addConnection(a, "");

        }

        void Session_End(Object sender, EventArgs E)
        {
            TableHub hub = new TableHub();
            hub.SessionEnd(this.Session.SessionID);
            //this.Session.SessionID
        }

    }
}