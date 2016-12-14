using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Web.Script.Serialization;
using Models;

namespace Hubs
{
    public class TableHub : Hub
    {

        #region Override Method

        public override System.Threading.Tasks.Task OnConnected()
        {
            Clients.All.newUserConnected("New User Online.");
            string id = Context.ConnectionId;
            string group = Context.Request.Cookies["ASP.NET_SessionId"].Value;

            Session.Instancia.updateConnectionIdByCookie(group, id);

            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            string id = Context.ConnectionId;
            string group = Context.Request.Cookies["ASP.NET_SessionId"].Value;
            Clients.All.newUserDisconnected("New User Online.");
            return base.OnDisconnected();
        }

        #endregion

        #region Public Method
        [CustomAuthorizeClaim]
        public void InsertRow(Person person)
        {
            try
            {
                
                Clients.All.receiveNewRow(new JavaScriptSerializer().Serialize(person));
            }
            catch (Exception) { }
        }
        public void UpdateRow(Person person)
        {
            Clients.All.receiveUpdatedRow(new JavaScriptSerializer().Serialize(person));
        }
        public void RemoveRow(int id)
        {
            Clients.All.receiveRemovedRow(id);
        }
        public void InitSession()
        {
            if (System.Web.HttpContext.Current.Session != null)
            {
                System.Web.HttpContext.Current.Session.Timeout = 1;
                if (System.Web.HttpContext.Current.Session["_TSession"] == null)
                {
                    System.Web.HttpContext.Current.Session["_TSession"] = System.Web.HttpContext.Current.Session["ABC"] = new TableHub();
                }
            }
        }
        public void SessionEnd(string sessionId)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<TableHub>();
            string connectionId = Session.Instancia.getConnectionIdByCookie(sessionId);
            //hubContext.Clients.All.sessionEnd(connectionId);
            hubContext.Clients.Client(connectionId).sessionEnd(connectionId);
        }


        #endregion

    }

    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

}