using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Web.Mvc;

namespace EApp.WebMvc.Plugin
{
    public class CompiledVirtualPathProvider : VirtualPathProvider
    {

        public override bool FileExists(string virtualPath)
        {
            return base.FileExists(virtualPath);
        }
    }
}
