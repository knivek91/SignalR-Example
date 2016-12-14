using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(SignalR.StartUp))]
namespace SignalR
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            //GlobalHost.HubPipeline.RequireAuthentication(); // For autenticated user in all the app
        }
    }
}