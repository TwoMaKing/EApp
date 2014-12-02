using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace EApp.Core
{
    public class ResourceManagerBase : IResourceManager
    {
        private ResourceManager resourceManager;

        public ResourceManagerBase(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        public object GetObject(string name)
        {
            return this.resourceManager.GetObject(name);
        }

        public object GetObject(string name, System.Globalization.CultureInfo culture)
        {
            return this.resourceManager.GetObject(name, culture);
        }

        public System.IO.UnmanagedMemoryStream GetStream(string name)
        {
            return this.resourceManager.GetStream(name);
        }

        public System.IO.UnmanagedMemoryStream GetStream(string name, System.Globalization.CultureInfo culture)
        {
            return this.resourceManager.GetStream(name, culture);
        }

        public System.Drawing.Image GetImage(string name)
        {
            object imageObject = this.resourceManager.GetObject(name);

            if (imageObject != null)
            {
                return (System.Drawing.Image)imageObject;
            }

            return null;
        }

        public System.Drawing.Image GetImage(string name, System.Globalization.CultureInfo culture)
        {
            object imageObject = this.resourceManager.GetObject(name, culture);

            if (imageObject != null)
            {
                return (System.Drawing.Image)imageObject;
            }

            return null;
        }

        public string GetString(string name)
        {
            return this.resourceManager.GetString(name);
        }

        public string GetString(string name, System.Globalization.CultureInfo culture)
        {
            return this.resourceManager.GetString(name, culture);
        }
    }
}
