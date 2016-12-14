using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hubs
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class CustomAuthorizeClaim : AuthorizeAttribute
    {
        // despues llama este
        protected override bool UserAuthorized(System.Security.Principal.IPrincipal user)
        {
            return true;
            //return base.UserAuthorized(user);
        }

        public override bool AuthorizeHubConnection(Microsoft.AspNet.SignalR.Hubs.HubDescriptor hubDescriptor, IRequest request)
        {
            
            return base.AuthorizeHubConnection(hubDescriptor, request);
        }

        // llama primero este
        public override bool AuthorizeHubMethodInvocation(Microsoft.AspNet.SignalR.Hubs.IHubIncomingInvokerContext hubIncomingInvokerContext, bool appliesToMethod)
        {
            string token = hubIncomingInvokerContext.Hub.Context.QueryString["uid"];

            if (token != "mytoken")
                return false;
            return true;
        }

    }
}