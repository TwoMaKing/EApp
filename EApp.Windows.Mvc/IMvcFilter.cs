using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Windows.Mvc
{
    public interface IMvcFilter
    {
        /// <summary>
        /// When implemented in a class, gets or sets a value that indicates whether
        /// multiple filters are allowed.  Return true if multiple filters are allowed; otherwise, false.
        /// </summary>
        bool AllowMultiple { get; }

        /// <summary>
        /// When implemented in a class, gets the filter order.
        /// </summary>
        int Order { get; }
    }
}
