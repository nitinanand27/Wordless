using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Wordless.Models;

namespace Wordless
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start()
        {
            //Id for which pdf to show
            Session["pdfIdToShow"] = 6;
            ///Id for the current logged in user(int)
            Session["currentUserId"] = "";
            ///Name for the current logged in user (string)
            Session["currentUsername"] = "";
            ///Logged in or not (bool)
            Session["loginStatus"] = false;
            Session["Admin"] = false;
            Session["isAuthor"] = false;
        }
    }
}
