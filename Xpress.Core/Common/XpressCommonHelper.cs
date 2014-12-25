using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using EApp.Common;
using EApp.Core;
using EApp.Core.Application;
using EApp.UI.Controls.GridView;

namespace Xpress.Core.Common
{
    public sealed class XpressCommonHelper
    {
        private XpressCommonHelper() { }

        public static Image GetResourceImage(ValueImage valueImage)
        {
            if (!EAppRuntime.Instance.CurrentApp.ResourceManagers.ContainsKey(valueImage.ResourceManagerName))
            {
                return null;
            }

            return EAppRuntime.Instance.CurrentApp.ResourceManagers[valueImage.ResourceManagerName].GetImage(valueImage.ImageName);
        }
    }
}
