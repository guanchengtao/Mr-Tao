using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SDAU.GCT.OA.UI.Portal
{
    public class MvcApplication :Spring.Web.Mvc.SpringMvcApplication //System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            //从配置文件读取配置，然后进行初始化工作
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
