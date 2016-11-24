using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Web.Script.Serialization;

namespace SignalR
{
    public class TableHub : Hub
    {

        #region Override Method

        public override System.Threading.Tasks.Task OnConnected()
        {
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            return base.OnDisconnected();
        }

        #endregion

        #region Public Method
        public void InsertRow(Person person)
        {
            try
            {
                Clients.All.receiveNewRow(new JavaScriptSerializer().Serialize(person));
            }
            catch (Exception) { /* Clients.Caller.dis */ }
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
        public void SessionEnd()
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<TableHub>();
            hubContext.Clients.All.sessionEnd();
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