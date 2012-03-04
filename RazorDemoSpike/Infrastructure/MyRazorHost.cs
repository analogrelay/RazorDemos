using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc.Razor;
using System.Web.Razor.Parser;

namespace RazorDemoSpike.Infrastructure
{
    public class MyRazorHost : MvcWebPageRazorHost
    {
        public MyRazorHost(string virtualPath, string physicalPath) : base(virtualPath, physicalPath) {
            NamespaceImports.Add("RazorDemoSpike.Infrastructure");
        }

        public override ParserBase DecorateCodeParser(ParserBase incomingCodeParser)
        {
            if (incomingCodeParser is CSharpCodeParser)
            {
                return new MyCSharpRazorCodeParser();
            }
            
            return base.DecorateCodeParser(incomingCodeParser);
        }
    }
}