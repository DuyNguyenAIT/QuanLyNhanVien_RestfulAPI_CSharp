using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace QuanLyNhanVien_Client
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

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();

            // Do something with the error.
            System.Diagnostics.Debug.WriteLine(exception);

            // Redirect somewhere or return an error code in case of web api
            Response.Redirect("/Error/PageNotFound");
        }
    }
}