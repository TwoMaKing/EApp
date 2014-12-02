using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Windows.Mvc
{
    public interface IViewDataContainer
    {
        /// <summary>
        /// Gets or sets the view data dictionary.
        /// </summary>
        IDictionary<string, object> ViewData { get; set; }
    }
}
