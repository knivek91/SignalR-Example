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
        public void InsertRow(Person person)
        {
            Clients.AllExcept(Context.ConnectionId).receiveNewRow(new JavaScriptSerializer().Serialize(person));
        }
        public void UpdateRow(Person person)
        {
            Clients.AllExcept(Context.ConnectionId).receiveUpdatedRow(new JavaScriptSerializer().Serialize(person));
        }
        public void RemoveRow(int id)
        {
            Clients.AllExcept(Context.ConnectionId).receiveRemovedRow(id);
        }
    }
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}