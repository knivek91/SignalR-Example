using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Home
    {


        #region Set Connection ID

        public bool setConnectionId(string pConnection)
        {
            System.Web.HttpContext.Current.Session["ConnectionId"] = pConnection;
            return true;
        }

        #endregion

        #region Get Connection ID
        public string getConnectionId()
        {
            return System.Web.HttpContext.Current.Session["ConnectionId"].ToString();
        }
        #endregion
    }
}