using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using NLog;
using StudentManager.AutoMapper;
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
            MapperConfig = new MapperConfiguration(cfg => 
            { 
                cfg.AddProfile(new StudentProfile());
            });

            //var config = new NLog.Config.LoggingConfiguration();

            //// Targets where to log to: File and Console
            //var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "file.txt" };
            //var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            //// Rules for mapping loggers to targets            
            //config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);
            //config.AddRule(LogLevel.Error,  LogLevel.Fatal, logfile);

            //// Apply config           
            //NLog.LogManager.Configuration = config;
        }
    }
}
