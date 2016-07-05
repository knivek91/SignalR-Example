using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace SignalR
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class AuthorizeHub : AuthorizeAttribute
    {
        protected override bool UserAuthorized(System.Security.Principal.IPrincipal user)
        {

            if (user == null)
                throw new ArgumentException("Invalid User");

            var principal = user as ClaimsPrincipal;


            if (principal == null)
                return false;

            Claim authenticated = principal.FindFirst(ClaimTypes.Authentication);

            if (authenticated == null && !authenticated.Value.Equals("true"))
                return false;

            return true;
        }
    }
}