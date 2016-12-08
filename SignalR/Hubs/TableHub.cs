using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Web.Script.Serialization;

namespace Hubs
{
    public class TableHub : Hub
    {

        #region Override Method

        public override System.Threading.Tasks.Task OnConnected()
        {
            Clients.All.newUserConnected("New User Online.");
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
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
                Models.Home homeModel = new Models.Home();
                homeModel.setConnectionId(Context.ConnectionId);
                
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
            Models.Home homeModel = new Models.Home();
            string connectionId = homeModel.getConnectionId();
            hubContext.Clients.User(connectionId).sessionEnd();
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