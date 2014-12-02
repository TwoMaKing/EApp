using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EApp.Core;
using EApp.Core.List;

namespace EApp.Core.Plugin
{
    public class NavigationNodeItem : IComparable<NavigationNodeItem>
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

        public int CompareTo(NavigationNodeItem other)
        {
            if (other == null)
            {
                return 1;
            }

            return this.name.CompareTo(other.name);
        }
    }
}
