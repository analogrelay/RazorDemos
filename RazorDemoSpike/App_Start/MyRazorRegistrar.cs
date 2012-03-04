using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Razor;
using System.Web.WebPages.Razor;
using RazorDemoSpike.Infrastructure;

[assembly: WebActivator.PreApplicationStartMethod(typeof(RazorDemoSpike.App_Start.MyRazorRegistrar), "Start")]
namespace RazorDemoSpike.App_Start
{
    public static class MyRazorRegistrar
    {
        public static void Start()
        {
            //RazorBuildProvider.CompilingPath += RazorBuildProvider_CompilingPath;
        }

        static void RazorBuildProvider_CompilingPath(object sender, CompilingPathEventArgs e)
        {
            // If MVC-Razor was already going to handle this
            //if (e.Host is MvcWebPageRazorHost)
            //{
            //    // Switch it out for our custom host.
            //    e.Host = new MyRazorHost(e.Host.VirtualPath, e.Host.PhysicalPath);
            //}
        }
    }
}