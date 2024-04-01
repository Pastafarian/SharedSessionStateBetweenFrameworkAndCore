using System;
using System.Configuration;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using MvcApp;

namespace AspNetWebApp
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SystemWebAdapterConfiguration.AddSystemWebAdapters(this)
                .AddProxySupport(options => options.UseForwardedHeaders = true)
                .AddJsonSessionSerializer(options => ClassLibrary.RemoteServiceUtils.RegisterSessionKeys(options.KnownKeys))
                .AddRemoteAppServer(options => options.ApiKey = ConfigurationManager.AppSettings["RemoteAppApiKey"])
                .AddAuthenticationServer()
                .AddSessionServer();

            //SystemWebAdapterConfiguration.AddSystemWebAdapters(this)
            //    .AddJsonSessionSerializer(options =>
            //    {
            //        // Serialization/deserialization requires each session key to be registered to a type
            //        options.RegisterKey<int>("FirstName");
            //        options.RegisterKey<Foo>("Foo");
            //    })
            //    // Provide a strong API key that will be used to authenticate the request on the remote app for querying the session
            //    // ApiKey is a string representing a GUID
            //    .AddRemoteAppServer(options => options.ApiKey = ConfigurationManager.AppSettings["RemoteAppApiKey"])
            //    .AddSessionServer();
        }
    }
}