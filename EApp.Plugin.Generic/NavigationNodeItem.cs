using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Common;
using EApp.Common.List;

namespace EApp.UI.Plugin
{
    public class NavigationNodeItem
    {
        private string name = string.Empty;
        private string text = string.Empty;

        public NavigationNodeItem(string name, string text) 
        {
            this.name = name;
            this.text = text;
        }

        public string Name 
        {
            get 
            {
                return this.name;
            }
        }

        public string Text 
        {
            get 
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }

    }
}
