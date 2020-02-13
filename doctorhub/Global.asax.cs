using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace doctorhub
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(object sender,EventArgs e)
        {
            doctorhubDataAccess.DirectoryDataAccess obj = new doctorhubDataAccess.DirectoryDataAccess();
            HttpContext.Current.Session["UserNTID"] = "HUBshiv";
          //  obj.SubmitUserTraffic();

        }
    }
}
