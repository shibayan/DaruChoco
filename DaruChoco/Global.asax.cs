using System;
using System.Web.Routing;

using DaruChoco.Hubs;

namespace DaruChoco
{
    public class Global : System.Web.HttpApplication
    {
        private string _path;

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapHubs();

            _path = Server.MapPath("~/count.csv");

            ChocoHub.Deserialize(_path);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            ChocoHub.Serialize(_path);
        }
    }
}