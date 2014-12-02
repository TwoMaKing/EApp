using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Windows.Mvc
{
    /// <summary>
    /// Represents the base class for action and result filter attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public abstract class FilterAttribute : Attribute, IMvcFilter
    {
        protected FilterAttribute() { }

        public bool AllowMultiple 
        {
            get { return false; } 
        }
        
        /// <summary>
        /// Gets or sets the order in which the action filters are executed.
        /// </summary>
        public int Order { get; set; }
    }
}
