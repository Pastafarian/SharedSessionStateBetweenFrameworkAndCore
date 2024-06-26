﻿using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ClassLibrary;

namespace Framework
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            SystemWebAdapterConfiguration
                .AddSystemWebAdapters(this)
                .AddProxySupport(options => options.UseForwardedHeaders = true)
                .AddJsonSessionSerializer(options =>
                {
                    options.RegisterKey<SessionDemoModel>("crumbs");
                    options.RegisterKey<string>("language");
                })
                .AddRemoteAppServer(options =>
                {
                    options.ApiKey = ConfigurationManager.AppSettings["RemoteAppApiKey"];
                })
                .AddAuthenticationServer(options =>
                {
                    //options.AuthenticationEndpointPath = "Account/Login";
                }).AddSessionServer();
        }
    }
}
