using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Session
    {

        #region Variable
        private static Session session = new Session();
        private static List<Aux> Sessiones = new List<Aux>();
        #endregion

        #region Propiedades
        public static Session Instancia
        {
            get {
                if (session == null) {
                    session = new Session();
                }
                return session;
            }
        }
        #endregion

        #region Constructor
        protected Session() { }
        #endregion

        #region Metodos Publicos

        #region Add New Connection
        public bool addConnection(string cookie, string connectionId = "")
        {
            try
            {
                if (cookie == "" || cookie == string.Empty)
                    throw new Exception("The value of the cookie can not be empty.");

                if (Sessiones == null)
                    Sessiones = new List<Aux>();

                if (Sessiones.Count == 0)
                {
                    Sessiones.Add(new Aux() { Cookie = cookie, ConnectionID = connectionId });
                }
                else
                {
                    if (Sessiones.Where(x => x.Cookie == cookie).ToList().Count > 0)
                        throw new Exception("The cookie id is registered.");
                }
                
            }
            catch (Exception ex) { throw ex; }
            return true;
        }
        #endregion

        #region Update Connection
        public bool updateConnectionIdByCookie(string cookie, string connectionId)
        {
            try
            {
                if (Sessiones == null)
                    throw new Exception("The connection list is empty.");

                Sessiones.ForEach(delegate(Aux aux)
                {
                    if (cookie == aux.Cookie)
                        aux.ConnectionID = connectionId;
                });

            }
            catch (Exception) { throw; }
            return true;
        }
        #endregion

        #region Delete Connection By Cookie
        public bool DeleteConnectionIdByCookie(string cookie)
        {
            try
            {
                if (Sessiones == null)
                    throw new Exception("The connection list is empty.");

                Sessiones = Sessiones.Where(x => x.Cookie == cookie).ToList<Aux>();

            }
            catch (Exception) { throw; }
            return true;
        }
        #endregion

        #region Delete Connection By Connection ID
        public bool DeleteConnectionIdByConnectionID(string connectionId)
        {
            try
            {
                if (Sessiones == null)
                    throw new Exception("The connection list is empty.");

                Sessiones = Sessiones.Where(x => x.ConnectionID == connectionId).ToList<Aux>();

            }
            catch (Exception) { throw; }
            return true;
        }
        #endregion

        #region Get Connection ID by Cookie
        public string getConnectionIdByCookie(string cookie)
        {
            try
            {
                if (Sessiones == null)
                    throw new Exception("The connection list is empty.");

                return ((Aux)Sessiones.Where(x => x.Cookie == cookie).Select( y => y).FirstOrDefault()).ConnectionID;

            }
            catch (Exception) { throw; }

        }
        #endregion

        #region Get ConnectionId By Connection ID
        public string getConnectionIdByConnectionID(string connectionId)
        {
            try
            {
                if (Sessiones == null)
                    throw new Exception("The connection list is empty.");

                return ((Aux)Sessiones.Where(x => x.ConnectionID == connectionId).Select(y => y).FirstOrDefault()).Cookie;

            }
            catch (Exception) { throw; }

        }
        #endregion
        
        #endregion

    }

    #region Clase Aux
    class Aux
    {
        public string Cookie { get; set; }
        public string ConnectionID { get; set; }

        public Aux() { }
    }
    #endregion

}