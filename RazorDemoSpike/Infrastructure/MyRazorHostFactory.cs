using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Razor;

namespace RazorDemoSpike.Infrastructure
{
    public class MyRazorHostFactory : System.Web.WebPages.Razor.WebRazorHostFactory
    {
        public override WebPageRazorHost CreateHost(string virtualPath, string physicalPath)
        {
            return new MyRazorHost(virtualPath, physicalPath);
        }
    }
}