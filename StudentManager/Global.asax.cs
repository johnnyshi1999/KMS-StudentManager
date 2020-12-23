using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using StudentManager.Models;

namespace StudentManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static MapperConfiguration MapperConfig { get; private set; }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<Student, StudentViewModel>());
        }
    }
}
